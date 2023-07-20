<script>
import carousel from './signinCarousel.vue';
export default {
  data() {
    return {
      account: '',
      password: '',
      confirmpassword: '',
      securityQ: '',
      securityAns: ''
    };
  },
  components: {
      'my-carousel': carousel
  },
  methods: {
    register() {
      if(this.password != this.confirmpassword) {
      alert('确认密码与新密码不一致');
      return;
    }
      // 在这里编写注册逻辑，可以发送请求将账号、密码、密保问题和密保答案提交到服务器
      console.log('账号:', this.account);
      console.log('密码:', this.password);
      console.log('密保问题:', this.securityQ);
      console.log('密保答案:', this.securityAns);
    },
    redirectToLogin() {
      // 跳转到登录页面的逻辑
      this.$router.push('/signin');
    },
  }
};
</script>

<template>
  <div class="common-layout">
    <el-container>
        <el-aside>
          <my-carousel />
        </el-aside>
        <el-main>
            <!-- 注册 -->
            <div style="height:10%">
              <div class="FontLayout">
                <p class="FontTypography">注册</p>
              </div>
            </div>
            <!-- 输入内容 -->
            <div style="height:70%">
              <form @submit.prevent="register">
                <!-- 输入账号 -->
                <div class="subBox">
                    <label for="account" class="subTittle subTittleTypography" >账号：</label>
                    <el-input 
                    type="text" 
                    id="account" 
                    v-model="account" 
                    pattern="[a-zA-Z0-9]+" 
                    required maxlength="10" 
                    class="inputBox inputTypography"
                    placeholder="账号只能由数字和字母组成，且长度不超过10个字符"
                    />
                </div>
                <!-- 输入密码 -->
                <div class="subBox">
                    <label for="password" class="subTittle subTittleTypography" >密码：</label>
                    <el-input 
                    type="password" 
                    id="password" 
                    v-model="password" 
                    pattern="[a-zA-Z0-9]+" 
                    required maxlength="15"
                    class="inputBox inputTypography"
                    show-password
                    placeholder="密码只能由数字和字母组成，且长度不超过15个字符"
                    />
                </div>
                <!-- 再次输入密码 -->
                <div class="subBox">
                    <label for="confirmpassword" class="subTittle subTittleTypography">确认密码：</label>
                    <el-input 
                    type="password" 
                    id="confirmpassword" 
                    v-model="confirmpassword" 
                    pattern="[a-zA-Z0-9]+" 
                    required maxlength="15"
                    class="inputBox inputTypography"
                    show-password
                    placeholder="请再次输入密码"
                    />
                </div>
                <!-- 密保问题 -->
                <div class="subBox">
                    <label for="securityQ" class="subTittle subTittleTypography">密保问题：</label>
                    <el-input 
                    type="text" 
                    id="securityQ" 
                    v-model="securityQ" 
                    pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
                    required maxlength="10"
                    class="inputBox inputTypography"
                    placeholder="密保问题只能包含中文、数字和标点符号，且长度不超过10个字符"
                    />
                </div>
                <!-- 密保答案 -->
                <div class="subBox">
                    <label for="securityAns" class="subTittle subTittleTypography">密保答案：</label>
                    <el-input 
                    type="text" 
                    id="securityAns" 
                    v-model="securityAns" 
                    pattern="[\u4e00-\u9fa5\d\s\p{P}]+"
                    required maxlength="10"
                    class="inputBox inputTypography"
                    placeholder="密保答案只能包含中文、数字和标点符号，且长度不超过10个字符"
                    />
                </div>
              </form>
            </div>
            <!-- 注册按钮 -->
            <div>
              <text @click="redirectToLogin" class="havePos haveText">
                已经有账号了？
              </text>
              <button type="submit" class="signupBtn">
                <text class="btnText">注册</text>
              </button>
            </div>
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
.el-main{
    position: absolute;
    top:0;
    right:0;
    width:50%;
    height:100%;
}
/*左上角图标＋文字的style*/
.logoBox{
    position: relative;
    top:4vw;
    left:8vw;
    width:2vw;
    height:2vw;
}
.footGameTypography{
    position: relative;
    top:3.8vw;
    left:10vw;
    color: #000;
    font-family: Poppins;
    font-size: 2rem;
    font-style: normal;
    font-weight: 600;
    line-height: 163%;
    letter-spacing: 0.54rem;
}

/*文字“注册”的style*/
.FontLayout{
    text-align: center;
    position:relative; 
    width: 13.4vw;
    top:0;
    height: 5vw;
    left:40%;
    flex-direction: column;
    flex-shrink: 0;
}
.FontTypography{
    color: #3E4772;
    font-family: Poppins;
    font-size: 2.5rem;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}
/*用来装入每一个子标题+输入框的box*/
.subBox{
    position:relative;
    right:10vw;
    left:5%;
    width: 25vw;
    height: 7vw;
    flex-shrink: 0;
}
/*每一个子标题的位置及样式*/
.subTittle{
    display: flex;
    position: relative;
    left:10vw;
    width: 10vw;
    height: 2.5vw;
    flex-direction: column;
    flex-shrink: 0;
    text-align: left;
}
.subTittleTypography{
    color: #000;
    font-family: Poppins;
    font-size: 1.2rem;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
}
/*每一个输入框的样式及内置提示*/
.inputBox{
    border: 0;
    position: relative;
    left: 9vw;
    width: 30vw;
    height: 2vw;
    flex-shrink: 0;
    text-align: center;
}
.inputTypography{
    border-radius: 3vw;
    background: #F1F1F1;
}
.holderTypography{
    position: relative;
    left:10vw;
    color:black;
    font-family: Poppins;
    font-size: 0.75rem;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
}
/* 注册按钮＋已经有账号了 */
.signupBtn{
  position: relative;
  left:45%;
  width: 13vw;
  height: 2.5vw;
  bottom: -4vw;
  flex-shrink: 0;
  border-radius: 3vw;
  background: #2E375D;
}
.btnText{
  color: #FFF;
  font-family: Poppins;
  font-size: 1rem;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}
.havePos{
  position: relative;
  left:43%;
  width: 0.5vw;
  height: 2.5vw;
  bottom: -4vw;
}
.haveText{
  cursor: pointer;
  color: #494F7A;
  font-family: Poppins;
  font-size: 1rem;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}
</style>
  