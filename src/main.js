import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import TLoading from "./components/base/TLoading.vue"

const app = createApp(App);
app.component("TLoading", TLoading);
app.use(ElementPlus)
app.mount('#app')

