<template>
  <div>
    <h1>登录</h1>
    <form @submit.prevent="register">
      <label for="account">账号：</label>
      <input type="text" id="account" v-model="account" pattern="[a-zA-Z0-9]+" required maxlength="10">
      <p>账号只能由数字和字母组成，且长度不超过10个字符</p>
      <br>
      <label for="password">密码：</label>
      <input type="password" id="password" v-model="password" pattern="[a-zA-Z0-9]+" required maxlength="15">
      <p>密码只能由数字和字母组成，且长度不超过15个字符</p>
      <br>
      <button type="submit">登录</button>
      <button @click="this.$router.push('/recover')">找回密码</button>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      account: '',
      password: '',
    };
  },
  methods: {
    register() {
      // 在这里编写注册逻辑，可以发送请求将账号和密码提交到服务器
      console.log('账号:', this.account);
      console.log('密码:', this.password);
    }
  }
};
</script>
