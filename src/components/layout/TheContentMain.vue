<template>
  <div class="t-content-main the-content" id="content">
    <!-- Content-header -->
    <div class="t-main-header">
      <div class="t-header-title">Nhân viên</div>
      <button class="t-button-add" id="btnAdd" @click="btnAddOnClick">
        Thêm mới nhân viên
      </button>
    </div>
    <div class="t-main-body">
      <!-- Search -->
      <div class="t-main-search">
        <div class="t-search-left">
          <el-dropdown trigger="click" class="dropdown-delete">
            <span class="dropdown-delete-link pointer" @click="showDeleteAll">
              <div class="delete-link">Thực hiện hàng loạt</div>
              <div class="icon-delete-all"></div>
            </span>
            <template #dropdown>
              <el-dropdown-menu v-if="isShowDelete">
                <el-dropdown-item @click="deleteSelected">Xoá</el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </div>
        <div class="t-search-right">
          <div class="t-search-gr">
            <input
              class="t-input"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-model="search"
              @keyup="searchData"
            />
            <div class="icon t-icon-search" @click="searchOnClick"></div>
          </div>

          <el-tooltip content="Lấy lại dữ liệu">
            <div
              class="icon t-icon-refresh mi-refresh"
              id="load-data"
              @click="refreshOnClick"
            ></div>
          </el-tooltip>
          <el-tooltip content="Xuất ra Excel">
            <div
              class="icon t-icon-refresh export"
              @click="exportOnClick"
            ></div>
          </el-tooltip>
        </div>
      </div>
      <!-- Table -->
      <div class="t-main-table">
        <div class="t-table">
          <table class="t-table-viewer">
            <thead>
              <tr>
                <th class="t-checkbox-td">
                  <input
                    class="t-checkbox"
                    type="checkbox"
                    v-model="selectAll"
                  />
                </th>
                <th class="width-170">MÃ NHÂN VIÊN</th>
                <th class="width-196">TÊN NHÂN VIÊN</th>
                <th class="width-137">GIỚI TÍNH</th>
                <th class="width-114">NGÀY SINH</th>
                <th class="width-138" title="Số chứng minh nhân dân">
                  SỐ CMND
                </th>
                <th class="width-150">NGÀY CẤP</th>
                <!-- <th class="width-141">NƠI CẤP</th> -->
                <th class="width-143">CHỨC DANH</th>
                <th class="width-244">TÊN ĐƠN VỊ</th>
                <th class="width-208">SỐ TÀI KHOẢN</th>
                <th class="width-189">TÊN NGÂN HÀNG</th>
                <th class="width-250" title="Chi nhánh tài khoản ngân hàng">
                  CHI NHÁNH TK NGÂN HÀNG
                </th>
                <th class="t-option">CHỨC NĂNG</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="emp in employees"
                :key="emp.EmployeeId"
                @dblclick="dblShowDialog(emp)"
              >
                <td class="t-checkbox-td checkbox">
                  <input
                    v-model="selected"
                    :value="emp.EmployeeID"
                    class="t-checkbox"
                    type="checkbox"
                  />
                </td>
                <td>{{ emp.EmployeeCode }}</td>
                <td>{{ emp.EmployeeName }}</td>
                <td>{{ formatGender(emp.Gender) }}</td>
                <td class="text-align">{{ formatDate(emp.DateOfBirth) }}</td>
                <td>{{ emp.IdentityNumber }}</td>
                <td class="text-align">{{ formatDate(emp.IdentityDate) }}</td>
                <!-- <td>{{ emp.IdentityPlace }}</td> -->
                <td>{{ emp.PositionName }}</td>
                <td>{{ emp.DepartmentName }}</td>
                <td>{{ emp.BankNumber }}</td>
                <td>{{ emp.BankName }}</td>
                <td>{{ emp.BankBranchName }}</td>
                <td class="t-option t-opiton-edit">
                  <el-dropdown
                    trigger="click"
                    @click="dblShowDialog(emp)"
                    split-button
                  >
                    <span class="el-dropdown-link"> Sửa </span>
                    <template #dropdown>
                      <el-dropdown-menu>
                        <el-dropdown-item @click="replicationEmployee(emp)"
                          >Nhân bản</el-dropdown-item
                        >
                        <el-dropdown-item @click="btnDeleteEmployee(emp)">
                          Xoá
                        </el-dropdown-item>
                        <el-dropdown-item>Ngưng sử dụng</el-dropdown-item>
                      </el-dropdown-menu>
                    </template>
                  </el-dropdown>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <!-- Footer - page -->
        <div class="t-footer">
          <div class="t-main-footer">
            <div class="t-footer-left">
              Tổng số: <b>{{ count }}</b> bản ghi
            </div>
            <div class="t-footer-right">
              <el-select
                placeholder="Chọn bản ghi trên 1 trang"
                class="sel-footer-right"
                v-model="pageSizing.id"
                size="large"
                :autocomplete="on"
                v-on:change="pageTest"
                :filterable="true"
              >
                <el-option
                  class="ops-footer-right"
                  v-for="item in pageSizing"
                  :key="item.id"
                  :label="item.title"
                  :value="item.id"
                />
              </el-select>
              <!-- Phân trang -->
              <div class="t-footer-page">
                <el-pagination
                  v-model:currentPage="currentPage"
                  v-model:page-size="pageSize"
                  @current-change="pageChangeClick"
                  @size-change="handleSizeChange"
                  :pager-count="4"
                  small
                  background
                  layout="prev, pager, next"
                  prev-text="Trước"
                  next-text="Sau"
                  :total="count"
                  class="mt-4"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <input ref="fileImport" id="fileImport" type="file" hidden />
  <TheDialogEmployee
    v-if="isShowDialog"
    @closeDialog="showDialog"
    @reFresh="reFresh"
    @showReLoad="showReLoad"
    :employeeSelected="employeesSelected"
    :editMode="editMode"
  />
  <TLoading v-if="isLoading" />
  <!-- Form thông báo khi xóa -->
  <div v-if="isDelete" class="t-dialog-error">
    <div class="t-content-dialog">
      <div class="t-header-error">
        <div class="t-icon-ms icon-warning"></div>
        <div class="t-ms-content">
          {{ isContentDelete }}
        </div>
      </div>
      <div class="t-ms-line"></div>
      <div class="t-footer-error">
        <div class="t-footer-close">
          <button class="btn-default button-error" @click="btnNoDelete">
            Không
          </button>
          <button class="btn button-error" @click="btnYesDelete">Có</button>
        </div>
      </div>
    </div>
  </div>
</template>
<style>
@import url(../../css/layout/content_main.css);
</style>
<script>
import MISAResource from '../../js/resource.js'
import MISAEnum from '../../js/enum.js'
import axios from "axios";
import TheDialogEmployee from "./TheDialogEmployee.vue";
import { ElMessage } from "element-plus";
export default {
  name: "ContentMainList",
  /****************************************
   * Tạo các biến và đối tượng
   * CreatedBy: VTPTHAO
   * CreatedDate:  20/06/2022
   */
  data() {
    return {
      employees: [], //danh sách nhân viên
      isShowDialog: false, //ẩn hiện form chi tiết nhân viên
      isDelete: false, //ẩn hiện cảnh báo xóa
      employeeSelected: {}, //nhân viên chọn
      editMode: 0, //thực hiện thêm
      isLoading: false, //show loading
      count: 0, //số lượng employee
      employeeDelete: {}, //nhân viên xóa
      search: "", //nội dung tìm kiếm
      pageSizing: [
        { id: 10, title: "10 bản ghi trên 1 trang" },
        { id: 20, title: "20 bản ghi trên 1 trang" },
        { id: 30, title: "30 bản ghi trên 1 trang" },
        { id: 50, title: "50 bản ghi trên 1 trang" },
        { id: 100, title: "100 bản ghi trên 1 trang" },
      ],
      currentPage: 1, //Trang hiện tại là 1,
      pageSize: 10, //số lượng bản ghi trên 1 trang
      isContentDelete: "", //nội dung message xóa
      selected: [], //mảng chứa id các nhân viên được chọn
      isShowDelete: false, //hiển thị context xóa hàng loạt
    };
  },
  components: {
    TheDialogEmployee,
  },
  methods: {
    reFresh(isReFresh) {
      if (isReFresh) setTimeout(() => this.getPaging(), 500);
    },
    /************************************************************
     * Hiển thị reload khi thực hiện thao tác thêm hoặc sửa
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    showReLoad(isShow) {
      if (!isShow) {
        return;
      }
      // hiển thị thông báo thực hiện thành công
      ElMessage({
        message: MISAResource.SuccessMsg,
        grouping: true,
        type: "success",
        duration: 2000,
      });

      //this.reLoad();
      setTimeout(() => this.reLoad(), 500);
    },
    /**********************************************************
     * Hiển thị context xóa hàng loạt
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    showDeleteAll() {
      if (this.selected.length != 0) {
        this.isShowDelete = true;
      }
    },
    /*********************************************************
     * Thay đổi sựa chọn số bản ghi trên 1 trang
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    pageTest() {
      this.pageSize = this.pageSizing.id;
      this.handleSizeChange();
    },
    /******************************************************
     * Trang thay đổi
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    pageChangeClick() {
      try {
        this.kpSearch();
      } catch (error) {
        console.log(error);
        alert(MISAResource.IsError);
      }
    },
    /*****************************************************
     * Số lượng bản ghi trên trang thay đổi
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    handleSizeChange() {
      try {
        this.kpSearch();
      } catch (error) {
        console.log(error);
        alert(MISAResource.IsError);
      }
    },
    /********************************************************
     * Gán dữ liệu cho danh sách, gán số lượng bản ghi
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     * @param {*} res
     */
    errorAndData(res) {
      if (!res) {
        console.log(MISAResource.IsError);
        return;
      }
      this.employees = res.data.Data;
      this.count = res.data.TotalRecord;
    },
    /****************************************************
     * Không đồng ý xóa
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    btnNoDelete() {
      this.isDelete = false;
      this.employeeDelete = {};
    },
    /******************************************************
     * Đồng ý xóa
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    async btnYesDelete() {
      var me = this;
      try {
        //Thực hiện xóa khi ấn nút xóa trong context menu
        if (me.employeeDelete.EmployeeCode != undefined) {
          axios
            .delete(
              `${MISAResource.Url.Employee}${me.employeeDelete.EmployeeID}`
            )
            .then(function (res) {
              console.log(res);
              ElMessage({
                message: MISAResource.DeleteSuccess,
                grouping: true,
                type: "success",
                duration: 2000,
              });
              me.isDelete = false;
              me.employeeDelete = {};
              me.reLoad();
            })
            .catch(function (res) {
              console.log(res);
            });
        } else {
          //Thực hiện xóa khi ấn nút xóa hàng loạt
          me.isDelete = false;
          await me.selected.forEach((element) => {
            axios
              .delete(`${MISAResource.Url.Employee}${element}`)
              .then(function (res) {
                console.log(res);
              })
              .catch(function (res) {
                console.log(res);
              });
          });
           me.reLoad();
          ElMessage({
            message: MISAResource.DeleteSuccess,
            grouping: true,
            type: "success",
            duration: 2000,
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**********************************
         * Định dạng dữ liệu ngày tháng năm
         * CreatedBy: VTPTHAO
         * CreatedDate:  19/06/2022
         * Input: Giá trị kiểu date
         * Output: - true: trả về giá trị được format
                   - false: trả về null
         */
    formatDate(dob) {
      try {
        let dateConvert = new Date(dob);
        if (
          dob &&
          dateConvert instanceof Date &&
          !isNaN(dateConvert.valueOf())
        ) {
          let date = dateConvert.getDate();
          date = date < 10 ? `0${date}` : date;
          // lay ra thang
          let month = dateConvert.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          // lay ra nam
          let year = dateConvert.getFullYear();
          dob = `${date}/${month}/${year}`;
          return dob;
        } else return "";
      } catch (error) {
        console.log(error);
        alert(error);
      }
    },
    /*********************************************
     * Định dạng dữ liệu giới tính
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    formatGender(gender) {
      try {
        let genderConvert;
        if (gender == MISAEnum.Gender.FeMale) genderConvert = MISAResource.Gender.FeMale;
        if (gender == MISAEnum.Gender.Male) genderConvert = MISAResource.Gender.Male;
        if (gender == MISAEnum.Gender.Other) genderConvert = MISAResource.Gender.Other;
        return genderConvert;
      } catch (error) {
        console.log(error);
      }
    },
    /**********************************
     * Hiển thị form chi tiết nhân viên
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    showDialog(isShow) {
      this.isShowDialog = isShow;
    },
    /*****************************************************
     * Load lại dữ liệu khi thực hiện thêm mới, sửa, xóa
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    reLoad() {
      var me = this;
      this.isLoading = true;
      axios
        .get(
          `${MISAResource.Url.Search}?pageIndex=${this.currentPage}&pageSize=${this.pageSize}&filter=${this.search}`
        )
        .then(function (res) {
          me.errorAndData(res);
          setTimeout(() => (me.isLoading = false), 1000);
        })
        .catch(function (res) {
          console.log(res);
        });
    },
    /******************************************************************
     * Khi nhấn vào icon refresh sẽ thực hiện reload lại dữ liệu trang
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    refreshOnClick() {
      this.reLoad();
    },
    /******************************************************************
     * Thực hiện chức năng tìm kiếm
     * Input: key: mã nhân viên EmployeeId
     * CreatedBy: VTPTHAO
     * CreatedDate:  19/06/2022
     */
    kpSearch() {
      try {
        var me = this;
        var key = this.search;
        setTimeout(function () {
          me.employees = {};
          me.isLoading = true;
          // Thực hiện tìm kiếm dữ liệu
          axios
            .get(
              `${MISAResource.Url.Search}?pageIndex=${me.currentPage}&pageSize=${me.pageSize}&filter=${key}`
            )
            .then(function (res) {
              if (res == null) return console.log(res);
              // me.employees = res.data.Data;
              me.isLoading = false;
              me.errorAndData(res);
            })
            .catch(function (err) {
              console.log(err);
            });
        }, 1000);
      } catch (error) {
        console.log(error);
      }
    },
    /**********************************************************
     * Chức năng nhân bản
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    replicationEmployee(emp) {
      this.editMode = MISAEnum.EditMode.Replication;
      // gán dữ liệu và hiển thị form thông tin nhân viên
      this.dataAssignment(emp);
    },
    /************************************************************
     * Gán dữ liệu cho employeeSelected và hiển thị form thông tin nhân viên
     * CreatedBy: VTPTHAO
     * @param {*} emp đối tượng cần gán
     */
    dataAssignment(emp) {
      this.employeesSelected = emp;
      this.isShowDialog = true;
    },
    /*************************************************************
     * Chức năng phân trang
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    getPaging() {
      var me = this;
      // Thực hiện tìm kiếm dữ liệu
      axios
        .get(
           `${MISAResource.Url.Search}?pageIndex=${me.currentPage}&pageSize=${me.pageSize}&filter=${this.search}`
        )
        .then(function (res) {
          if (res == null) return console.log(res);
          me.errorAndData(res);
        })
        .catch(function (err) {
          console.log(err);
        });
    },

    /*******************************************
     * Search  dữ liệu
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    searchData() {
      setTimeout(() => this.getPaging(), 1000);
    },

    /**************************************************
     * Thực hiện chức năng xuất khẩu
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    exportOnClick() {
      window.open(`${MISAResource.Url.Export}`);
      // console.log(this.selected.length);
    },
    /*****************************************************
     * DoubleClick vào 1 phần tử trên table hoặc nút sửa ở cột chức năng sẽ hiện lên thông tin chi tiết tương ứng và cho phép thực hiện nút cất
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     */
    dblShowDialog(emp) {
      try {
        if (emp) {
          this.employeesSelected = emp;
          this.isShowDialog = true;
          this.editMode = MISAEnum.EditMode.Update;
          this.employeeDelete = emp;
        } else {
          console.log(emp);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**********************************************
     * Khi nhấn vào nút thêm mới nhân viên thì hiện form chi tiết nhân viên, mã NV tự động sinh và focus vào ô nhập liệu đầu tiên
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     * Input: biến isShowDialog
     * Output: - true : hiện form
     *         - false: ẩn form
     */
    btnAddOnClick() {
      try {
        this.showDialog(true);
        this.editMode = MISAEnum.EditMode.Add;
        this.employeesSelected = {};
      } catch (error) {
        console.log(error);
      }
    },
    /*********************************************
     * Khi nhấn vào nút xóa thực hiện xóa thông tin nhân viên với id tương ứng
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     */
    btnDeleteEmployee(emp) {
      this.isContentDelete = MISAResource.QuestionDelete.ContentDeleteLeft + emp.EmployeeCode + MISAResource.QuestionDelete.ContentDeleteRight;
      this.employeeDelete = emp;
      this.isDelete = true;
    },
    /*************************************************************
     * kiểm tra chọn hết phần tử không
     * output: true - các phần tử đã được lưu hết trong mảng selected
     *         false - các phần tử chưa được lưu hết trong mảng selected
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    checkSelectAll() {
      var check = true;
      this.employees.forEach((element) => {
        if (this.selected.includes(element.EmployeeID) == false) check = false;
      });
      return check;
    },
    /**************************************
     * Xóa hàng loạt
     * CreatedBy: VTPTHAO
     * CreatedDate:  17/07/2022
     */
    deleteSelected() {
      this.isContentDelete =
        MISAResource.QuestionDelete.All;
      this.isDelete = true;
    },

    /******************************************************************
     * Khi nhấn vào icon dropdown cạnh button sửa sẽ hiển thị contextmenu
     * CreatedBy: VTPTHAO
     * CreatedDate: 10/07/2022
     */
    onClick(event) {
      if (!event.target.matches(".t-icon-drop") && this.IsShowContext)
        this.IsShowContext = false;
    },
  },

  /************************************************
   * Thực hiện load dữ liệu từ api đổ về table
   * CreatedBy: VTPTHAO
   * CreatedDate:  19/06/2022
   * Output: - true: trả về danh sách nhân viên
   *         - false: null
   */
  created() {
    try {
      this.reLoad();
    } catch (error) {
      console.log(error);
      alert(MISAResource.IsError);
    }
  },
  /***************************************
   * Kiểm soát sự thay đổi của selectAll
   * CreatedBy: VTPTHAO
   * CreatedDate:  17/07/2022
   */
  computed: {
    selectAll: {

      get: function () {
        return this.employees.length ? this.checkSelectAll() : false;
      },
      set: function (value) {
        var selected1 = [];
        // nếu có dữ liệu
        if (value) {
          this.employees.forEach(function (emp) {
            selected1.push(emp.EmployeeID);
          });
        }

        // nếu các phần tử chưa được chọn hết thì lưu những phần tử chưa tồn tại trong mảng selected
        if (!this.checkSelectAll()) {
          selected1.forEach((element) => {
            if (!this.selected.includes(element)) this.selected.push(element);
          });
        } else {
          // thực hiện xoá những phần tử tương ứng trong trang hiện tại khi bỏ tích
          this.employees.forEach(function (emp) {
            selected1.push(emp.EmployeeID);
          });

          selected1.forEach((element) => {
            let index = this.selected.indexOf(element);
            this.selected.splice(index, 1);
          });
        }
      },
    },
  },
  /********************************************************
   * Không hiển thị xóa hàng loạt khi không chọn phần tử nào
   * CreatedBy: VTPTHAO
   * CreatedDate:  17/07/2022
   */
  updated() {
    if (this.selected.length == 0) {
      this.isShowDelete = false;
    }
  },
};
</script>
<style lang="scss" scoped>
.the-content {
  ::v-deep {
    .el-button {
      border: none !important;
    }
    .el-button.el-dropdown__caret-button {
      border: none !important;
      &::before {
        background: none;
      }
    }
  }
}
</style>
