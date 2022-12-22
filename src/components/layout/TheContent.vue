<template>
  <div class="t-content">
    <TheContentHeader/>
    <TheContentMain/>
  </div>
</template>

<script>
import TheContentHeader from './TheContentHeader.vue'
import TheContentMain from './TheContentMain.vue'
export default {
  name: 'TheContent',
  components: {
    TheContentHeader, TheContentMain
  }
}
</script>
<style>
@import url(../../css/layout/content.css);
</style>
