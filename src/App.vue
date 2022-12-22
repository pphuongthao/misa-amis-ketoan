<template>
  <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
  <div class="container">
    <TheMenu/>
    <TheContent/>
  </div>
</template>

<script>
import TheMenu from './components/layout/TheMenu.vue'
import TheContent from './components/layout/TheContent.vue' 
export default {
  name: 'App',
  components: {
    TheMenu, TheContent
  }
}
</script>
<style>
@import url(./css/main.css);
</style>
