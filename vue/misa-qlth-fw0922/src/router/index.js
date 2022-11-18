import { createRouter, createWebHashHistory } from 'vue-router'
import EmployeeList from '../view/EmployeeList.vue'
import ReportCpn from '../view/ReportList.vue'
import DeviceCpn from '../view/DeviceList.vue'

const routes = [
    // {   path: '/',  component: App    },
    {   path: '/employee', component: EmployeeList  },
    {   path: '/report',  component: ReportCpn    },
    {   path: '/device',  component: DeviceCpn    },
]
const router = createRouter ({
    history: createWebHashHistory(process.env.BASE_URL),
    routes
})

export default router