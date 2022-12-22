<template>
  <div class="t-dialog">
    <div
      class="popup-employee"
      id="dlgEmployeeDetail"
      v-loading="loading"
      element-loading-background="transparent"
    >
      <!-- Header form -->
      <header class="popup-header">
        <div class="popup-title">
          <div class="title-popup">{{ formName }}</div>
          <label for="" class="header-checkbox">
            <input type="checkbox" id="" class="check-input" />
            <div class="checkbox-title">Là khách hàng</div>
          </label>
          <label for="" class="header-checkbox">
            <input type="checkbox" id="" class="check-input" />
            <div class="checkbox-title">Là nhà cung cấp</div>
          </label>
        </div>
        <div class="popup-button">
          <el-tooltip content="Giúp">
            <div class="button-help has-tooltip"></div>
          </el-tooltip>
          <el-tooltip content="Đóng">
            <div
              class="button-close has-tooltip"
              id="btnClose"
              @click="btnCloseDialogOnClick"
            ></div>
          </el-tooltip>
        </div>
      </header>
      <!-- Content form -->
      <div class="popup-content">
        <div class="popup-form">
          <div class="top-form">
            <!-- Top-left -->
            <div class="left-top">
              <div class="row-input">
                <div class="input-box">
                  <label for="" class="input-label required">Mã</label>
                  <input
                    type="text"
                    class="input-info code input-info-required"
                    title="Mã không được để trống!"
                    id="txtEmployeeCode"
                    v-model="employee.EmployeeCode"
                    ref="refEmployeeCode"
                    :class="{ 'input-valid': arrayInputColors[0].isValid }"
                    @blur="blurColor"
                    maxlength="20"
                  />
                </div>
                <div class="input-box">
                  <label for="info_name" class="input-label required"
                    >Tên</label
                  >
                  <input
                    type="text"
                    class="input-info name input-info-required"
                    title="Tên không được để trống!"
                    id="txtEmployeeName"
                    maxlength="100"
                    v-model="employee.EmployeeName"
                    ref="refEmployeeName"
                    :class="{ 'input-valid': arrayInputColors[1].isValid }"
                    @blur="blurColor"
                  />
                </div>
              </div>
              <div class="row-input">
                <div class="input-box">
                  <label for="" class="input-label required">Đơn vị</label>
                    <el-select
                      class="sel-footer-right"
                      v-model="employee.DepartmentId"
                      placeholder="Chọn đơn vị"
                      size="large"
                      :autocomplete="on"
                      :filterable="true"
                      ref="refDepartmentId"
                      :class="{ 'input-valid': arrayInputColors[2].isValid }"
                      @blur="blurColor"
                    >
                      <el-option
                        class="ops-footer-right"
                        v-for="item in departments"
                        :key="item.DepartmentId"
                        :label="item.DepartmentName"
                        :value="item.DepartmentId"
                      />
                    </el-select>
                </div>
              </div>
              <div class="row-input">
                <div class="input-box">
                  <label for="info_position" class="input-label"
                    >Chức danh</label
                  >
                  <input
                    type="text"
                    class="input-info position"
                    maxlength="128"
                    v-model="employee.PositionName"
                  />
                </div>
              </div>
            </div>
            <!-- Top-right -->
            <div class="right-top">
              <div class="row-input">
                <div class="input-box">
                  <label for="" class="input-label">Ngày sinh</label>
                  <input
                    type="date"
                    class="input-info dob"
                    id=""
                    v-model="dateOfBirth"
                    placeholder="DD/MM/YYYY"
                    ref="refDateOfBirth"
                    :class="{ 'input-valid': arrayInputColors[3].isValid }"
                    @blur="blurColor"
                  />
                </div>
                <div class="input-box">
                  <label for="" class="input-label">Giới tính</label>
                  <div class="input-box-radio">
                    <div class="radio-gender">
                      <input
                        type="radio"
                        name="info-gender"
                        id="nam"
                        v-model="employee.Gender"
                        value="1"
                        checked
                      />
                      <label class="option-radio" for="nam">Nam</label>
                    </div>
                    <div class="radio-gender">
                      <input
                        type="radio"
                        name="info-gender"
                        id="nu"
                        v-model="employee.Gender"
                        value="0"
                      />
                      <label class="option-radio" for="nu">Nữ</label>
                    </div>
                    <div class="radio-gender">
                      <input
                        type="radio"
                        name="info-gender"
                        id="khac"
                        v-model="employee.Gender"
                        value="2"
                      />
                      <label class="option-radio" for="khac">Khác</label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="row-input">
                <div class="input-box">
                  <el-tooltip
                    content="Số chứng minh nhân dân"
                    placement="bottom-start"
                  >
                    <label for="" class="input-label">Số CMND</label>
                  </el-tooltip>
                  <input
                    type="text"
                    class="input-info cmnd"
                    id="txtEmployeeCMND"
                    maxlength="25"
                    v-model="employee.IdentityNumber"
                    ref="refIdentityNumber"
                    :class="{ 'input-valid': arrayInputColors[4].isValid }"
                    @blur="blurColor"
                  />
                </div>
                <div class="input-box">
                  <label for="info_name" class="input-label">Ngày cấp</label>
                  <input
                    type="date"
                    class="input-info idendate"
                    id="txtIdentityDate"
                    v-model="identityDate"
                    ref="refIdentityDate"
                    :class="{ 'input-valid': arrayInputColors[5].isValid }"
                    @blur="blurColor"
                  />
                </div>
              </div>
              <div class="row-input">
                <div class="input-box">
                  <label for="info_name" class="input-label">Nơi cấp</label>
                  <input
                    type="text"
                    class="input-info"
                    id="txtIdentityPlace"
                    maxlength="255"
                    v-model="employee.IdentityPlace"
                  />
                </div>
              </div>
            </div>
          </div>
          <!-- Bottom-form -->
          <div class="bottom-form">
            <div class="row-input">
              <div class="input-box" style="padding-right: 0">
                <label for="info_name" class="input-label">Địa chỉ</label>
                <input
                  type="text"
                  class="input-info"
                  id="txtEmployeeAddress"
                  maxlength="255"
                  v-model="employee.Address"
                />
              </div>
            </div>
            <div class="row-input">
              <div class="input-box">
                <el-tooltip
                  content="Điện thoại di động"
                  placement="bottom-start"
                >
                  <label for="info_name" class="input-label">ĐT di động</label>
                </el-tooltip>
                <input
                  type="text"
                  class="input-info"
                  id="txtPhoneNumber"
                  maxlength="50"
                  v-model="employee.PhoneNumber"
                />
              </div>
              <div class="input-box">
                <el-tooltip
                  content="Điện thoại cố định"
                  placement="bottom-start"
                >
                  <label for="info_name" class="input-label">ĐT cố định</label>
                </el-tooltip>
                <input
                  type="text"
                  class="input-info"
                  id="txtPhoneStatic"
                  maxlength="50"
                  v-model="employee.PhoneStatic"
                />
              </div>
              <div class="input-box">
                <label for="info_name" class="input-label">Email</label>
                <input
                  type="text"
                  class="input-info"
                  id="txtEmail"
                  maxlength="100"
                  v-model="employee.Email"
                  ref="refEmail"
                  :class="{ 'input-valid': arrayInputColors[6].isValid }"
                  @blur="blurColor"
                />
              </div>
              <div class="input-box"></div>
            </div>
            <div class="row-input">
              <div class="input-box">
                <label for="info_name" class="input-label"
                  >Tài khoản ngân hàng</label
                >
                <input
                  type="text"
                  class="input-info"
                  id="txtBankNumber"
                  maxlength="25"
                  v-model="employee.BankNumber"
                  ref="refBankNumber"
                  :class="{ 'input-valid': arrayInputColors[7].isValid }"
                  @blur="blurColor"
                />
              </div>
              <div class="input-box">
                <label for="info_name" class="input-label">Tên ngân hàng</label>
                <input
                  type="text"
                  class="input-info"
                  id="txtBankName"
                  maxlength="255"
                  v-model="employee.BankName"
                />
              </div>
              <div class="input-box">
                <label for="info_name" class="input-label">Chi nhánh</label>
                <input
                  type="text"
                  class="input-info"
                  id="txtBankBranchName"
                  maxlength="255"
                  v-model="employee.BankBranchName"
                />
              </div>
              <div class="input-box"></div>
            </div>
          </div>
        </div>
        <!-- Button hủy, cất và thêm -->
        <div class="popup-footer">
          <div class="divide"></div>
          <div class="footer-container">
            <div class="right-button">
              <button
                class="btn-default button-store"
                id="btnSave"
                @click="btnSave"
              >
                Cất
              </button>
              <button
                class="btn button-add"
                id="btnSaveAndAdd"
                @click="btnSaveAndAdd"
              >
                Cất và Thêm
              </button>
            </div>
            <div class="left-button">
              <button
                class="btn-default button-cancel"
                id=""
                @click="btnCancelDialog"
              >
                Hủy
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Form thông báo lỗi Validate -->
  <div v-if="isValidate" class="t-dialog-error">
    <div class="t-content-dialog">
      <div class="t-header-error">
        <div class="t-icon-ms icon-validate"></div>
        <div class="t-ms-content">
          <div v-for="mess in messages" :key="mess">{{ mess }}</div>
        </div>
      </div>
      <div class="t-ms-line"></div>
      <div class="t-footer-error">
        <div class="t-footer-content">
          <button class="btn button-error" @click="btnCloseWarning">
            Đóng
          </button>
        </div>
      </div>
    </div>
  </div>
  <!-- Form thông báo khi người dùng thoát mà chưa lưu dữ liệu -->
  <div v-if="isValidateClose" class="t-dialog-error">
    <div class="t-content-dialog">
      <div class="t-header-error">
        <div class="t-icon-ms icon-question"></div>
        <div class="t-ms-content">
          <div><span>Dữ liệu đã bị thay đổi. Bạn có muốn cất không?</span></div>
        </div>
      </div>
      <div class="t-ms-line"></div>
      <div class="t-footer-error">
        <div class="t-footer-close">
          <div>
            <button
              class="btn-default button-error-cancel"
              @click="btnCancelQuestion"
            >
              Hủy
            </button>
          </div>
          <div class="t-error-right">
            <button class="btn-default button-error-no" @click="btnNoQuestion">
              Không
            </button>
            <div class="button-yes">
              <button class="btn button-error-yes" @click="btnYesQuestion">
                Có
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Form thông báo khi trùng mã -->
  <div v-if="isDuplicate" class="t-dialog-error">
    <div class="t-content-dialog">
      <div class="t-header-error">
        <div class="t-icon-ms icon-warning"></div>
        <div class="t-ms-content">
          {{ isContentDuplicate }}
        </div>
      </div>
      <div class="t-ms-line"></div>
      <div class="t-footer-error">
        <div class="t-footer-duplicate">
          <button class="btn button-error" @click="btnCloseWarningCode">
            Đồng ý
          </button>
        </div>
      </div>
    </div>
  </div>
  <TLoading v-if="isLoading" />
</template>
<style>
@import url(../../css/layout/popup_eployee.css);
@import url(../../css/base/message.css);
</style>
<script>
import MISAResource from "../../js/resource.js";
import MISAEnum from "../../js/enum.js";
import axios from "axios";
import { ElMessage } from "element-plus";
// import { ElLoading } from 'element-plus';
export default {
  name: "EmployeeDetail",
  data() {
    return {
      employee: {}, //đối tượng nhân viên
      employees: [], //mảng đối tượng nhân viên
      messages: [], //mảng message thông báo
      errorMessage: false, // có lỗi validate thiếu dữ liệu (sai định dạng) hay không
      isError: false, //lỗi trùng mã
      isOK: false, //biến lỗi thêm có transaction, sửa xóa
      isContentDuplicate: "", //nội dung cảnh báo trùng mã
      isDuplicate: false, //hiển thị cảnh báo trùng mã
      isEdit: 0, //0 - thêm mới, 1 - sửa
      contentMessage: "", //nội dung thông báo
      isLoading: false, //ẩn hiện loading
      isDataChanged: false, // kiểm tra dữ liệu form có thay đổi không
      isValidate: false, //hiển thị form lỗi validate
      isValidateClose: false, //hiển thị form cảnh báo dữ liệu thay đổi
      departments: [], //mảng phòng ban
      dateOfBirth: "", //trung gian ngày sinh
      identityDate: "", //trung gian ngày cấp
      formName: "", //tên form khi thay đổi,
      arrayInputColors: [
        { id: "refEmployeeCode", isValid: false, value: "" },
        { id: "refEmployeeName", isValid: false, value: "" },
        { id: "refDepartmentId", isValid: false, value: "" },
        { id: "refDateOfBirth", isValid: false, value: "" },
        { id: "refIdentityNumber", isValid: false, value: "" },
        { id: "refIdentityDate", isValid: false, value: "" },
        { id: "refEmail", isValid: false, value: "" },
        { id: "refBankNumber", isValid: false, value: "" },
      ], // mảng các input (id - tên các input, isValid: true - không hợp lệ, false - hợp lệ)
      loading: false, //loading form chi tiết
    };
  },
  //nhận dữ liệu từ cha
  props: {
    //thông tin nhân viên khi doubleclick
    employeeSelected: {
      type: Object,
    },
    //phân biệt chức năng thêm, sửa, nhân bản
    editMode: {
      type: Number,
      default: 0, //0-thêm mới, 1-sửa, 2-nhân bản
      required: true,
    },
  },
  methods: {
    /*********************************************************
     * Khi ấn vào icon close sẽ kiểm tra xem dữ liệu có bị thay đổi hay không
     * - Nếu có sự thay đổi sẽ hiện form cảnh báo
     * - Nếu không sẽ thực hiện đóng form
     *  CreatedBy: VTPTHAO
     *  CreatedDate:  10/07/2022
     */
    btnCloseDialogOnClick() {
      if (this.isDataChanged) this.isValidateClose = true;
      else {
        this.$emit("closeDialog", false);
      }
      // this.isValidateClose = true;
    },
    /**************************************************
     * Thực hiện validate các trường dữ liệu bắt buộc nhập
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     * Input: các biến cần kiểm tra
     * Output: false: đưa ra cảnh báo
     */
    validateObject() {
      //Kiểm tra dữ liệu nhập đủ chưa
      if (!this.employee.EmployeeCode) {
        this.messages.push(MISAResource.Error.MsgEmployeeCode);
        this.errorMessage = true;
        this.arrayInputColors[0].isValid = true;
      }
      if (!this.employee.EmployeeName) {
        this.messages.push(MISAResource.Error.MsgEmployeeName);
        this.errorMessage = true;
        this.arrayInputColors[1].isValid = true;
      }
      if (!this.employee.DepartmentId) {
        this.messages.push(MISAResource.Error.MsgDepartmentId);
        this.errorMessage = true;
        this.arrayInputColors[2].isValid = true;
      }
      // kiểm tra đầu vào ngày tháng phải nhỏ hơn ngày tháng hiện tại
      if (new Date(this.dateOfBirth) > new Date()) {
        this.errorMessage = true;
        this.messages.push(MISAResource.Error.MsgDateOfBirth);
        this.arrayInputColors[3].isValid = true;
      }
      // kiểm tra đầu vào ngày tháng phải nhỏ hơn ngày tháng hiện tại
      if (new Date(this.identityDate) > new Date()) {
        this.errorMessage = true;
        this.messages.push(MISAResource.Error.MsgIdentityDate);
        this.arrayInputColors[5].isValid = true;
      }
      // kiểm tra đầu vào email
      if (this.employee.Email) {
        var validateRegex =
          /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/;
        if (!this.employee.Email.match(validateRegex)) {
          this.errorMessage = true;
          this.messages.push(MISAResource.Error.MsgEmail);
          this.arrayInputColors[6].isValid = true;
        }
      }
      //Nếu dữ liệu thiếu hiển thị message lỗi
      if (this.errorMessage) {
        this.isValidate = true;
        return false;
      }
      // kiểm tra trùng mã nhân viên
      if (!this.errorMessage) {
        this.employees.forEach((element) => {
          if (
            element.EmployeeCode == this.employee.EmployeeCode &&
            element.EmployeeID != this.employee.EmployeeID
          ) {
            this.isError = true;
            this.isContentDuplicate =
              MISAResource.ExistCode.ContentExistLeft +
              this.employee.EmployeeCode +
              MISAResource.ExistCode.ContentExistRight;
          }
        });
      }

      // Nếu có lỗi trùng mã hiển thị cảnh báo
      if (this.isError) {
        this.isDuplicate = true;
        return false;
      }
      return true;
    },
    /*************************************8
     * Format ngày tháng
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    formatDate(dob) {
      let dateConvert = new Date(dob);
      if (dob && dateConvert instanceof Date && !isNaN(dateConvert.valueOf())) {
        let date = dateConvert.getDate();
        date = date < 10 ? `0${date}` : date;
        // lay ra thang
        let month = dateConvert.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        // lay ra nam
        let year = dateConvert.getFullYear();
        dob = `${year}-${month}-${date}`;
        return dob;
      } else return "";
    },
    /**************************************************************
     * Gán dữ liệu cho đối tượng employee, ngày sinh, ngày cấp
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    formatDataIn() {
      this.$nextTick(() => this.$refs.refEmployeeCode.focus());
      this.employee = JSON.parse(JSON.stringify(this.employeeSelected));
      this.dateOfBirth = this.formatDate(this.employee.DateOfBirth);
      this.identityDate = this.formatDate(this.employee.IdentityDate);
    },
    /**********************************************************
     * Gán dữ liệu cho ngày sinh, ngày cấp và giới tính
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    formatDataOut() {
      if (this.dateOfBirth != "") this.employee.DateOfBirth = this.dateOfBirth;
      if (this.identityDate != "")
        this.employee.IdentityDate = this.identityDate;
      this.employee.Gender = parseInt(this.employee.Gender);
    },
    /***************************************************************
     * Kiểm tra dữ liệu thay đổi khi thêm mới
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    checkDataChange() {
      for (let [key, value] of Object.entries(this.employee)) {
        if (key != "EmployeeCode" && key != "Gender") {
          if (value != "") {
            this.isDataChanged = true;
            return;
          }
        }
      }
    },
    /*****************************************************
     * Khi nhấn vào nút hủy thì thực hiện đóng form chi tiết nhân viên
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     */
    btnCancelDialog() {
      this.$emit("closeDialog", false);
    },
    /*********************************************************
     * Đóng form thông báo trùng mã
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    btnCloseWarningCode() {
      this.isDuplicate = false;
      this.isError = false;
      this.isContentDuplicate = "";
    },
    /**************************************************
     * Khi nhấn vào nút cất sẽ thực hiện thêm thông tin của nhân viên
     * Điều kiện: đúng định dạng validate và mã nhân viên không được trùng
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     */

    async btnSave() {
      var me = this;
      try {
        await me.send();
      } catch (error) {
        console.log(error);
        alert(MISAResource.IsError);
      }
      //Nếu thành công
      if (!this.isOK && !this.isError && !this.errorMessage) {
        this.$emit("showReLoad", true);
        this.$emit("closeDialog", false);
      }
    },
    /**************************************************
     * Khi nhấn vào nút cất và thêm sẽ thực hiện thêm thông tin của nhân viên và tự sinh mã mới
     * Điều kiện: đúng định dạng validate và mã nhân viên không được trùng
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/06/2022
     */
    async btnSaveAndAdd() {
      try {
        await this.send();
        //reset form nếu không có lỗi
        if (!this.isOK && !this.isError && !this.errorMessage) {
          // this.loading = true;
          if (!this.isEdit) this.contentMessage = MISAResource.InsertSuccess;
          else {
            this.contentMessage = MISAResource.UpdateSuccess;
            this.formName = MISAResource.FormNameInsert;
            this.isEdit = 0;
          }
          //Hiển thị messsage thông báo thao tác thành công
          ElMessage({
            message: this.contentMessage,
            grouping: true,
            type: "success",
            duration: 2000,
          });
          this.employee = {};
          //Lấy mã nhân viên mới
          this.dateOfBirth = "";
          this.identityDate = "";
          this.$emit("reFresh", true);
          // setTimeout(() => (this.loading = false), 1000);
          await this.getNewEmployeeCode();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /*********************************************
     * Sự kiện ấn nút hủy thông báo question
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    btnCancelQuestion() {
      this.isValidateClose = false;
      this.isDataChanged = false;
    },
    /*************************************************
     * Sự kiện ấn nút không ở thông báo question
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    btnNoQuestion() {
      this.isValidateClose = false;
      this.isDataChanged = false;
      this.$emit("closeDialog", false);
    },
    /**********************************************************
     * Sự kiện ấn nút có ở thông báo question - đồng ý cất
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    btnYesQuestion() {
      this.isValidateClose = false;
      this.btnSave();
    },
    /******************************************************
     * Khi nhấn vào nút đóng ở dialog thông báo lỗi validate
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    btnCloseWarning() {
      this.isValidate = false;
      this.errorMessage = false;
      this.messages = [];
      // tìm vị trí lỗi đầu tiên và focus
      var i = 0;
      while (
        i < this.arrayInputColors.length &&
        !this.arrayInputColors[i].isValid
      ) {
        i++;
      }
      this.$refs[this.arrayInputColors[i].id].focus();
    },
    /************************************************************
     * Lấy mã nhân viên mới
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    async getNewEmployeeCode() {
      var me = this;
      this.loading = true;
      await axios
        .get(`${MISAResource.Url.Employee}${MISAEnum.EndPoint.NewCode}`)
        .then(function (res) {
          if (!res) {
            console.log(MISAResource.IsError);
            return;
          }
          me.employee.EmployeeCode = res.data;
          me.$refs.refEmployeeCode.focus();
          setTimeout(() => (me.loading = false), 500);
        })
        .catch(function (res) {
            me.messages = [];
            const statusCode = res.response.status;
            switch (statusCode) {
              case 500:
                me.messages.push(res.response.data.userMsg);
                break;
              case 400:
                me.messages = res.response.data.data.errors;
                break;
              default:
                break;
            }
            me.isValidate = true;
        });
    },
    /*******************************************************************
     * Chức năng làm đổi màu input khi dữ liệu validate thành công
     * CreatedBy: VTPTHAO
     * CreatedDate:  20/07/2022
     */
    blurColor() {
      this.arrayInputColors.forEach((element) => {
        var validateRegex =
          /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/;
        if (
          element.value &&
          element.isValid &&
          element.id != "refDateOfBirth" &&
          element.id != "refIdentityDate" &&
          element.id != "refEmail"
        )
          element.isValid = false;
        if (
          element.id == "refDateOfBirth" &&
          element.isValid &&
          new Date(this.dateOfBirth) < new Date()
        ) {
          element.isValid = false;
        }
        if (
          element.id == "refIdentityDate" &&
          element.isValid &&
          new Date(this.identityDate) < new Date()
        ) {
          element.isValid = false;
        }
        if (
          element.id == "refEmail" &&
          element.isValid &&
          this.employee.Email.match(validateRegex)
        ) {
          element.isValid = false;
        }
      });
    },
    /************************************************************
     * Thực hiện gửi dữ liệu lên server (thêm mới hoặc update)
     * CreatedBy: VTPTHAO
     * CreatedDate:  10/07/2022
     */
    async send() {
      var me = this;
      if (!this.validateObject()) return;
      //Thêm mới
      if (!this.isValidate && !this.isEdit && !this.isDuplicate) {
        //định dạng lại dữ liệu
        this.formatDataOut();
        await axios
          .post(`${MISAResource.Url.Employee}`, me.employee)
          .then(function (res) {
            me.isOK = false;
            console.log(res);
          })
          .catch(function (res) {
            me.isOK = true;
            me.messages = [];
            const statusCode = res.response.status;
            switch (statusCode) {
              case 500:
                me.messages.push(res.response.data.userMsg);
                break;
              case 400:
                me.messages = res.response.data.data.errors;
                break;
              default:
                break;
            }
            me.isValidate = true;
          });
      }
      //Cập nhật dữ liệu
      if (!this.isValidate && this.isEdit) {
        //định dạng lại dữ liệu
        this.formatDataOut();
        await axios
          .put(`${MISAResource.Url.Employee}`, me.employee)
          .then(function (res) {
            me.isOK = false;
            console.log(res);
          })
          .catch(function (res) {
            me.isOK = true;
            me.messages = [];
            const statusCode = res.response.status;
            switch (statusCode) {
              case 500:
                me.messages.push(res.response.data.userMsg);
                break;
              case 400:
                me.messages = res.response.data.data.errors;
                break;
              default:
                break;
            }
            me.isValidate = true;
          });
      }
    },
  },

  /***********************************************
   * CreatedBy: VTPTHAO
   * CreatedDate:  20/06/2022
   */
  created() {
    this.isEdit = this.editMode;
    if (this.editMode == MISAEnum.EditMode.Replication)
      this.isEdit = MISAEnum.EditMode.Add;
    var me = this;
    try {
      //lấy tất cả nhân viên
      axios
        .get(`${MISAResource.Url.Employee}`)
        .then(function (res) {
          if (!res) {
            console.log(MISAResource.IsError);
            return;
          }
          me.employees = res.data;
        })
        .catch(function (res) {
          console.log(res);
        });
      //lấy phòng ban từ database
      axios
        .get(`${MISAResource.Url.Departments}`)
        .then(function (res) {
          if (!res) {
            console.log(MISAResource.IsError);
            return;
          }
          me.departments = res.data;
        })
        .catch(function (res) {
          console.log(res);
        });
      //Thêm mới
      if (this.editMode == MISAEnum.EditMode.Add) {
        this.employee.Gender = MISAEnum.Gender.Male;
        this.getNewEmployeeCode();
        this.formName = MISAResource.FormNameInsert;
      }
      //Cất dữ liệu
      if (this.editMode == MISAEnum.EditMode.Update) {
        this.formatDataIn();
        this.$nextTick(() => this.$refs.refEmployeeCode.focus());
        this.formName = MISAResource.FormNameUpdate;
      }
      //Nhân bản
      if (this.editMode == MISAEnum.EditMode.Replication) {
        this.getNewEmployeeCode();
        this.formatDataIn();
        this.formName = MISAResource.FormNameInsert;
      }
    } catch (error) {
      console.log(error);
      alert(error);
    }
  },
  updated() {
    var me = this;
    //Nếu là sửa dữ liệu
    if (me.isEdit == MISAEnum.EditMode.Update) {
      if (me.employee != JSON.parse(JSON.stringify(me.employeeSelected))) {
        me.isDataChanged = true;
      }
    }
    //Nếu là thêm mới
    if (!me.isEdit) {
      this.checkDataChange();
    }

    this.arrayInputColors[0].value = this.employee.EmployeeCode;
    this.arrayInputColors[1].value = this.employee.EmployeeName;
    this.arrayInputColors[2].value = this.employee.DepartmentId;
    this.arrayInputColors[3].value = this.dateOfBirth;
    this.arrayInputColors[4].value = this.employee.IdentityNumber;
    this.arrayInputColors[5].value = this.identityDate;
    this.arrayInputColors[6].value = this.employee.Email;
    this.arrayInputColors[7].value = this.employee.BankAccountNumber;

    if (me.employee.DepartmentId) this.arrayInputColors[2].isValid = false;
    if (this.employee.DepartmentId != undefined)
      this.arrayInputColors[2].isValid = false;
  },
};
</script>
