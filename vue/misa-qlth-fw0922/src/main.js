import { createApp } from 'vue'
import App from './App.vue'

// import { createRouter, createWebHashHistory } from 'vue-router'
// import EmployeeList from './components/view/EmployeeList.vue'
// import ReportCpn from './components/view/ReportCpn.vue'
// import DeviceCpn from './components/view/DeviceCpn.vue'
import router from './router'

// import ElementPlus from 'element-plus'
// import 'element-plus/dist/index.css'
// import vi from 'element-plus/es/locale/lang/vi'  // Bỏ npm package thưu viện này đi

// const routes = [
//     // {   path: '/',  component: App    },
//     {   path: '/employee', component: EmployeeList    },
//     {   path: '/report',  component: ReportCpn    },
//     {   path: '/device',  component: DeviceCpn    }
// ]
// const router = createRouter ({
//     history: createWebHashHistory(process.env.BASE_URL),
//     routes
// })
// createApp(App).use(router)

const app = createApp(App)
app.use(router);
// app.use(ElementPlus, {
//     locale: vi
// });  // Bỏ npm package thư viện này đi
app.mount('#app')