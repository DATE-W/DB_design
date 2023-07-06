<template>
    <el-header>
    <my-nav></my-nav>
    </el-header>
</template>
  
<script>
  import MyNav from './nav.vue';
  
  export default {
    components: {
      'my-nav': MyNav
    }
  };
</script>

  
<!-- <button @click="this.$router.push('/signin')">登录</button>
<button @click="this.$router.push('/signup')">注册</button> -->
