<template>
    <el-header>
      <div class="nav-container">
        <div class="nav-left">
          <div class="nav-logo">Logo</div>
          <el-menu class="nav-menu"
            mode="horizontal" 
            active-text-color= "#409eff"
            @select="handleMenuSelect">
            <el-menu-item index="1" @click="redirectToMain">首页</el-menu-item>
            <el-menu-item index="2" @click="redirectToNews">新闻</el-menu-item>
            <el-menu-item index="3" @click="redirectToForum">论坛</el-menu-item>
            <el-menu-item index="4" @click="redirectToGames"> 赛事</el-menu-item>
            <el-menu-item index="5" @click="redirectToPlayers">球员信息</el-menu-item>
          </el-menu>
        </div>
        <div class="nav-right">
        <el-dropdown trigger="hover">
          <span class="user-avatar" @click="redirectToPersonal">
            <img src="avatar.jpg" alt="Avatar" />
          </span>
          <template #dropdown>
            <el-dropdown-menu v-slot: dropdown>
              <el-dropdown-item @click="redirectToLogin">登录</el-dropdown-item>
              <el-dropdown-item @click="redirectToRegister">注册</el-dropdown-item>
              <el-dropdown-item @click="redirectToPersonal">个人中心</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
        <span class="user-nickname">user-nickname</span>
      </div>
      </div>
    </el-header>
  </template>
  

<script>
export default {
  methods: {
    redirectToLogin() {
      // 跳转到登录页面的逻辑
      this.$router.push('/signin');
    },
    redirectToRegister() {
      // 跳转到注册页面的逻辑
      this.$router.push('/signup');
    },
    redirectToForum() {
      //跳转到论坛页面的逻辑
      this.$router.push('/forum')
    },
    redirectToMain(){
      //跳转到首页页面的逻辑
      this.$router.push('/')
    },
    redirectToPersonal(){
      //跳转到个人中心页面的逻辑
      this.$router.push('/personal')
    },
    redirectToGames(){
      //跳转到赛事页面的逻辑
      this.$router.push('/Games')
    },
    redirectToNews(){
      //跳转到新闻页面的逻辑
      this.$router.push('/News')
    },
    redirectToPlayers(){
      //跳转到新闻页面的逻辑
      this.$router.push('/Players')
    }
  }
};
</script>
  <style>
  .nav-container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 60px;
    padding: 0 20px;
  }

  .nav-menu{

  }

  .nav-left {
    display: flex;
    align-items: center;
  }
  
  .nav-logo {
    font-size: 20px;
    font-weight: bold;
    margin-right: 20px;
  }
  
  .nav-right {
    display: flex;
    align-items: center;
  }
  
  .user-avatar {
    display: inline-block;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    overflow: hidden;
    margin-right: 10px;
  }
  
  .user-nickname {
    margin-right: 20px;
  }
  </style>
  