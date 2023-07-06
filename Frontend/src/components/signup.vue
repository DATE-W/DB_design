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

<template>
  <div class="common-layout">
    <el-container>
      <el-aside>
        
      </el-aside>
      <el-main>
        <!-- 注册字体 -->
        <div class="FontLayout">
          <p class="FontTypography">注册</p>
        </div>
        <!-- 输入内容 -->
        <form @submit.prevent="register">
          <!-- 输入账号 -->
          <div class="subBox" style="top:170px;">
            <label for="account" class="subTittle subTittleTypography" >账号：</label>
            <input 
            type="text" 
            id="account" 
            v-model="account" 
            pattern="[a-zA-Z0-9]+" 
            required maxlength="10" 
            class="inputBox inputTypography"
            placeholder="账号只能由数字和字母组成，且长度不超过10个字符"
            >
          </div>
          <!-- 输入密码 -->
          <div class="subBox" style="top:270px;">
            <label for="password" class="subTittle subTittleTypography" >密码：</label>
            <input 
            type="password" 
            id="password" 
            v-model="password" 
            pattern="[a-zA-Z0-9]+" 
            required maxlength="15"
            class="inputBox inputTypography"
            placeholder="密码只能由数字和字母组成，且长度不超过15个字符"
            >
          </div>
          <!-- 输入密码 -->
          <div class="subBox" style="top:370px;">
            <label for="confirmpassword" class="subTittle subTittleTypography">确认密码：</label>
            <input 
            type="password" 
            id="confirmpassword" 
            v-model="confirmpassword" 
            pattern="[a-zA-Z0-9]+" 
            required maxlength="15"
            class="inputBox inputTypography"
            >
          </div>
          <!-- 密保问题 -->
          <div class="subBox" style="top:470px;">
            <label for="securityQ" class="subTittle subTittleTypography">密保问题：</label>
            <input 
            type="text" 
            id="securityQ" 
            v-model="securityQ" 
            pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
            required maxlength="10"
            class="inputBox inputTypography"
            placeholder="密保问题只能包含中文、数字和标点符号，且长度不超过10个字符"
            >
          </div>
          <!-- 密保答案 -->
          <div class="subBox" style="top:570px;">
            <label for="securityAns" class="subTittle subTittleTypography">密保答案：</label>
            <input 
            type="text" 
            id="securityAns" 
            v-model="securityAns" 
            pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
            required maxlength="10"
            class="inputBox inputTypography"
            placeholder="密保答案只能包含中文、数字和标点符号，且长度不超过10个字符"
            >
          </div>
          <!-- 注册按钮 -->
          <button type="submit">注册</button>
        </form>
      </el-main>
    </el-container>
  </div>
</template>

<style scoped>
.el-aside{
  position: absolute;
  background: linear-gradient(180deg, #77B0FE 0%, rgba(119, 176, 254, 0.10) 100%);
  top:0;
  left:0;
  width:50%;
  height:100%;
}
/*文字“注册”的style*/
.FontLayout{
  display: flex;
  position:absolute;
  width: 134px;
  height: 89px;
  top:10px;
  right:144px;
  flex-direction: column;
  flex-shrink: 0;
}
.FontTypography{
  color: #3E4772;
  font-family: Poppins;
  font-size: 60px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}
/*用来装入每一个子标题+输入框的box*/
.subBox{
  position:absolute;
  right:100px;
  width: 600px;
  height: 70px;
  flex-shrink: 0;
}
/*每一个子标题的位置及样式*/
.subTittle{
  display: flex;
  position: relative;
  left:450px;
  width: 100px;
  height: 44.917px;
  flex-direction: column;
  flex-shrink: 0;
  text-align: right;
}
.subTittleTypography{
  color: #000;
  font-family: Poppins;
  font-size: 20px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}
/*每一个输入框的样式及内置提示*/
.inputBox{
  border: 0;
  position: absolute;
  left: 150px;
  width: 400px;
  height: 40px;
  flex-shrink: 0;
  text-align: center;
}
.inputTypography{
  border-radius: 30px;
  background: #F1F1F1;
}
</style>
  