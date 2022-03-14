<template>
  <div>
      <NavbarComponent 
        v-bind:isCollapseMenu = "isCollapseMenu"
      />
      <HeaderComponent 
        v-on:onClickToggleNavbar = "onClickToggleNavbar"
      />
      <ContentComponent 
        v-bind:isCollapseMenu = "isCollapseMenu"

      />
  </div>
</template>

<script>
import NavbarComponent from './components/Layout/TheNavbar.vue'
import HeaderComponent from './components/Layout/TheHeader.vue'
import ContentComponent from './components/Layout/TheContent.vue'

export default {
  name: 'App',
  components: {
    NavbarComponent,
    HeaderComponent,
    ContentComponent
  },
  data() {
    return {
      isCollapseMenu : false
    }
  },

  methods: {

    /**
     * Sự kiện sử dụng để Thu gọn hoặc mở rộng thanh Navbar
     * CreatedBy : DDHung (20/11/2021)
     */
    onClickToggleNavbar : function()
    {
      this.isCollapseMenu = !this.isCollapseMenu;
    }
  },
 
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
@import url('./assets/css/main.css');
</style>
