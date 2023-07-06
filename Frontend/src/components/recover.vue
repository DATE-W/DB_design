<template>
  <div>
    <h1>找回密码</h1>
    <p>{{ securityQ }}</p>
    <label for="securityAns">密保答案：</label>
    <input type="text" id="securityAns" v-model="securityAns" required>
    <br>
    <label for="newPword">输入新密码：</label>
    <input id="newPword" v-model="newPword" :type="passwordVisible ? 'text' : 'password'"
      required>
    <br>
    <label for="confirmPword">确认新密码：</label>
    <input id="confirmPword" v-model="confirmPword" :type="passwordVisible ? 'text' : 'password'"
      required>
    <br>
    <button @click="ShoworHide">{{ passwordVisible ? '隐藏密码' : '显示密码' }}</button>
    <br>
    <button @click="recoverPword">确认修改</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      securityQ: '密保问题', // 替换为实际的密保问题
      securityAns: '',
      newPword: '',
      confirmPword: '',
      passwordVisible: false
    };
  },
  methods: {
    ShoworHide() {
      this.passwordVisible = !this.passwordVisible;
      console.log(passwordVisible);
    },
    recoverPword() {
      if(this.newPword !== this.confirmPword) {
        alert('确认密码与新密码不一致');
        return;
      }
      // 在这里编写找回密码的逻辑，可以发送请求将密保答案和新密码提交到服务器进行验证和修改
      console.log('密保答案:', this.securityAns);
      console.log('新密码:', this.newPword);
      console.log('确认密码:', this.confirmPword);
    }
  }
};
</script>
