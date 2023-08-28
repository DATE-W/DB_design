<template>
  <el-header>
    <div class="nav-container">
      <div class="nav-left">
        <div class="nav-logo">Logo</div>
        <el-menu class="nav-menu" mode="horizontal" active-text-color="#409eff" :ellipsis="false">
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
            <img :src=this.avatarurl alt="Avatar" />
          </span>
          <template #dropdown>
            <el-dropdown-menu v-slot: dropdown>
              <el-dropdown-item @click="redirectToLogin(0)" v-if="!this.islog">用户登录</el-dropdown-item>
              <el-dropdown-item @click="redirectToLogin(1)" v-if="!this.islog">管理员登录</el-dropdown-item>
              <el-dropdown-item @click="redirectToRegister" v-if="!this.islog">注册</el-dropdown-item>
              <el-dropdown-item @click="redirectToPersonal" v-if="this.islog">个人中心</el-dropdown-item>
              <el-dropdown-item @click="logout" v-if="this.islog">登出</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
        <span class="user-nickname">{{ this.userName }}</span>
      </div>
    </div>
  </el-header>
</template>
  

<script>
import axios from 'axios';
import { ElMessage } from 'element-plus';
export default {
  data() {
    return {
      islog: false,
      avatarurl: "./src/assets/img/carousel1.png",
      userName: '',
    }
  },
  mounted() {
    this.JudgeAccount();
  },
  methods: {
    async JudgeAccount() {
      const token = localStorage.getItem('token');
      if (token == null) {
        this.islog = false;
        console.log(this.islog);
        return;
      }
      let response
      try {
        const headers = {
          Authorization: `Bearer ${token}`,
        };
        response = await axios.post('/api/User/profile', {}, { headers })
      } catch (err) {
        console.log(err);
        if (err.response.data.result == 'fail') {
          ElMessage({
            message: err.response.data.msg,
            grouping: false,
            type: 'error',
          })
        } else {
          ElMessage({
            message: '未知错误',
            grouping: false,
            type: 'error',
          })
          return
        }
        return
      }
      console.log(response);
      //有账号
      if (response.data.ok == 'yes') {
        this.islog = true;
        this.userName = response.data.value.username;
        this.avatarurl = response.data.value.avatar;
      }
      else {
        this.islog = false;
      }
    },
    redirectToLogin(isAdmin) {
      // 跳转到登录页面的逻辑
      this.$router.push({
                path: '/signin',
                query: { 
                  isAdmin: isAdmin 
                }
          });
    },
    redirectToRegister() {
      // 跳转到注册页面的逻辑
      this.$router.push('/signup');
    },
    redirectToForum() {
      //跳转到论坛页面的逻辑
      this.$router.push('/forum')
    },
    redirectToMain() {
      //跳转到首页页面的逻辑
      this.$router.push('/')
    },
    redirectToPersonal() {
      //跳转到个人中心页面的逻辑
      this.$router.push('/personal')
    },
    redirectToGames() {
      //跳转到赛事页面的逻辑
      this.$router.push('/Games')
    },
    redirectToNews() {
      //跳转到新闻页面的逻辑
      this.$router.push('/News')
    },
    redirectToPlayers() {
      //跳转到新闻页面的逻辑
      this.$router.push('/Players')
    },
    logout() {
      localStorage.removeItem('token');
      this.$router.push('/');
      setTimeout(() => {
        window.location.reload(); // 刷新当前页面
      }, 100); // 2000毫秒后刷新，你可以根据需要调整延迟时间
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

.user-nickname {
  margin-right: 20px;
}
</style>
  