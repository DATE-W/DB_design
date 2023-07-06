<template>
    <el-header>
      <div class="nav-container">
        <div class="nav-left">
          <div class="nav-logo">Logo</div>
          <el-menu
            mode="horizontal"
            active-text-color="#409eff"
            @select="handleMenuSelect"
          >
            <el-menu-item index="1">首页</el-menu-item>
            <el-menu-item index="2">新闻</el-menu-item>
            <el-menu-item index="3">社区</el-menu-item>
            <el-menu-item index="4">赛事</el-menu-item>
            <el-menu-item index="5">球员信息</el-menu-item>
          </el-menu>
        </div>
        <div class="nav-right">
          <el-dropdown trigger="hover">
            <span class="user-avatar">
              <img src="avatar.jpg" alt="Avatar" />
            </span>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item>登录</el-dropdown-item>
              <el-dropdown-item>注册</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
          <span class="user-nickname">用户名</span>
        </div>
      </div>
    </el-header>
  </template>
  
  <script>
  export default {
    methods: {
      handleMenuSelect(index) {
        // 处理导航菜单选择事件
        console.log('Selected menu item:', index);
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
  
  .user-avatar img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
  
  .user-nickname {
    margin-right: 20px;
  }
  </style>
  