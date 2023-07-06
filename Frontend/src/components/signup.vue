<template>
    <div>
      <h1>注册</h1>
      <form @submit.prevent="register">
        <label for="account">账号：</label>
        <input type="text" id="account" v-model="account" pattern="[a-zA-Z0-9]+" required maxlength="10">
        <p>账号只能由数字和字母组成，且长度不超过10个字符</p>
        <br>
        <label for="password">密码：</label>
        <input type="password" id="password" v-model="password" pattern="[a-zA-Z0-9]+" required maxlength="15">
        <p>密码只能由数字和字母组成，且长度不超过15个字符</p>
        <label for="confirmpassword">确认密码：</label>
        <input type="password" id="confirmpassword" v-model="confirmpassword" pattern="[a-zA-Z0-9]+" required maxlength="15">
        <br><br>
        <label for="securityQ">密保问题：</label>
        <input type="text" id="securityQ" v-model="securityQ" pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
          required maxlength="10">
        <p>密保问题只能包含中文、数字和标点符号，且长度不超过10个字符</p>
        <br>
        <label for="securityAns">密保答案：</label>
        <input type="text" id="securityAns" v-model="securityAns" pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
          required maxlength="10">
        <p>密保答案只能包含中文、数字和标点符号，且长度不超过10个字符</p>
        <br>
        <button type="submit">注册</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        account: '',
        password: '',
        securityQ: '',
        securityAns: ''
      };
    },
    methods: {
      register() {
        if(this.password !== this.confirmpassword) {
        alert('确认密码与新密码不一致');
        return;
      }
        // 在这里编写注册逻辑，可以发送请求将账号、密码、密保问题和密保答案提交到服务器
        console.log('账号:', this.account);
        console.log('密码:', this.password);
        console.log('密保问题:', this.securityQ);
        console.log('密保答案:', this.securityAns);
      }
    }
  };
  </script>
  