<<<<<<< Updated upstream
<template>
    <p>登录</p>
</template>
=======
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

<template>
  <div class="common-layout">
    <el-container>
      <el-aside>
         <!-- 注册字体 -->
         <div class="FontLayout">
          <p class="FontTypography">登录</p>
        </div>
        <!-- 输入内容 -->
        <form @submit.prevent="register">
          <!-- 输入账号 -->
          <div class="subBox" style="top: 30vh;">
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
          <div class="subBox" style="top:50vh;">
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
          <!-- 登入按钮 -->
          <button class="signupBtn" style="left: 7vw; bottom: 15vh;">
            <text class="btnText">登录</text>
          </button>
          <!-- 注册按钮 -->
          <button class="signupBtn" style="left: 21vw; bottom: 15vh;">
            <text class="btnText">注册</text>
          </button>
        </form>
      </el-aside>

      <el-main>
        <!-- 左上角图标与文档 -->
        <text class="IconText" style="top:5vh;left:20vh">FootGame
        </text>
        <svg width="43" height="41" viewBox="0 0 43 41" fill="none" xmlns="http://www.w3.org/2000/svg" style="position:absolute;top:6vh;left:10vh">
        <path d="M31.2612 28.7053L33.7044 27.5843C34.646 29.4311 35.91 31.0682 37.4455 32.4956C40.3299 29.0682 42.069 24.7295 42.069 20.0037C42.069 15.2859 40.3384 10.9553 37.471 7.53592C35.961 8.93108 34.7054 10.5279 33.7722 12.3343L31.3375 11.1972C32.4234 9.10044 33.8825 7.2456 35.6471 5.63269C31.8635 2.15689 26.7226 0.00366211 21.0387 0.00366211C15.3634 0.00366211 10.2224 2.14882 6.43887 5.62463C8.19492 7.22947 9.65406 9.08431 10.7399 11.173L8.30521 12.3101C7.37204 10.5117 6.12498 8.91495 4.61495 7.51979C1.7306 10.9472 0 15.2779 0 20.0037C0 24.7295 1.7306 29.0601 4.61495 32.4795C6.15892 31.052 7.43142 29.415 8.37307 27.5601L10.8163 28.6811C9.72193 30.8343 8.24582 32.7375 6.44735 34.3827C10.2224 37.8585 15.3634 40.0037 21.0387 40.0037C26.7056 40.0037 31.838 37.8666 35.6216 34.3988C33.8316 32.7537 32.3555 30.8585 31.2612 28.7053ZM11.8937 26.1246L9.30624 25.3343C10.5703 21.5924 10.3836 17.7698 9.25534 14.5279L11.8343 13.7214C13.1068 17.3988 13.3613 21.7779 11.8937 26.1246ZM30.2432 13.7537L32.8221 14.5601C31.7023 17.794 31.5072 21.6166 32.7712 25.3666L30.1838 26.1569C28.7162 21.802 28.9707 17.423 30.2432 13.7537V13.7537Z" fill="black"/>
        </svg> 
        <!-- 锋线球员无背景组合图1 -->
        <div class="imgBox" style="">
        <img src="../assets/img/Striker1.png" class="img1">
        </div>
      </el-main>
    </el-container>
  </div>
</template>

<style scoped>
.el-main{
  position: absolute;
  background: linear-gradient(180deg, #77B0FE 0%, rgba(119, 176, 254, 0.10) 100%);
  top:0;
  right:0;
  width:61.8%;
  height:100%;
}
/*文字“注册”的style*/
.FontLayout{
  display: flex;
  position:absolute;
  width: 134px;
  height: 89px;
  top:10px;
  left:100px;
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
  left:100px;
  width: 600px;
  height: 70px;
  flex-shrink: 0;
}
/*每一个子标题的位置及样式*/
.subTittle{
  display: flex;
  position: relative;
  left:0px;
  width: 100px;
  height: 44.917px;
  flex-direction: column;
  flex-shrink: 0;
  text-align: left;
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
  left: 0px;
  width: 400px;
  height: 40px;
  flex-shrink: 0;
  text-align: center;
}
.inputTypography{
  border-radius: 30px;
  background: #F1F1F1;
}
/* 注册按钮 */
.signupBtn{
  position: absolute;
  width: 180px;
  height: 35px;
  flex-shrink: 0;
  border-radius: 30px;
  background: #2E375D;
}
.btnText{
  color: #FFF;
  font-family: Poppins;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}
/*图标后FootGame字体*/
.IconText{
  color: #000;
  position: absolute;
  font-family: Poppins;
  font-size: 36px;
  font-style: normal;
  font-weight: 600;
  line-height: 163%;
  letter-spacing: 5.4px;
}
/*锋线图片1样式*/
.imgBox{
  top: 13vh;
  width: 60vw;
  height: 80vh;
  position: relative;
}
.img1{
  width: 100%;
  height: 100%;
  object-fit: contain;
}
</style>
  
>>>>>>> Stashed changes
