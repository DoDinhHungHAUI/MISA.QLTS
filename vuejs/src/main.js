import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import {routes} from './routes'
Vue.use(VueRouter)
Vue.config.productionTip = false
Vue.config.productionTip = false

const router = new VueRouter({
  mode: 'history',
  base: __dirname,
  routes
})

router.beforeEach((to ,from , next) => {
  console.log(to.name);
  console.log(from);
  if(to.name)
  {
    document.title = `MISA - ${to.name}`
  }
  else{
    document.title = `MISA QLTS`
  }
  next();
})

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
