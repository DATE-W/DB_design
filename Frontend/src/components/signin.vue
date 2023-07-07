<!-- 2154314_郑楷_登录界面 2023.07.07 15:00 v1.0.1
 v1.0.0 画出页面
 v1.0.1 给出版本信息-->

<template>
  <div class="common-layout">
    <el-container>
      <el-aside style="width: 50%;">
        <img src="../assets/img/recover_logo.png" class="imgR">
        <!-- 注册字体 -->
        <div class="FontLayout">
          <el-space direction="vertical" style="text-align:center">
                <h1 class="labelH1 excenter">Login to your Account</h1>    
                <el-text tag="i" class="excenter" style="font-size: 0.9rem;">See popular football news and trends</el-text>
              </el-space>
        </div>
        <!-- 输入内容 -->
        <form @submit.prevent="register">
          <!-- 输入账号 -->
          <div class="subBox" style="top: 38vh;">
            <label for="account" class="inputText">账号：</label>
            <el-input type="text" id="account" v-model="account" pattern="[a-zA-Z0-9]+" required maxlength="10"
              class="inputBox" placeholder="账号只能由数字和字母组成，且长度不超过10个字符"></el-input>
          </div>
          <!-- 输入密码 -->
          <div class="subBox" style="top:50vh;">
            <label for="password" class="inputText">密码：</label>
            <el-input type="password" id="password" v-model="password" pattern="[a-zA-Z0-9]+" required maxlength="15"
              class="inputBox" placeholder="密码只能由数字和字母组成，且长度不超过15个字符" show-password></el-input>
          </div>
          <!-- 登入按钮 -->
          <button class="signupBtn" style="left: 12vw; bottom: 28vh;">
            <text class="btnText">登录</text>
          </button>
          
          <!-- 找回密码 -->
          <p class="tag" @click="redirectToRecover">Forgot Password?</p>
        </form>
        <!-- 注册按钮 -->
        <button class="signupBtn" style="left: 27vw; bottom: 28vh;" @click="redirectToRegister">
            <text class="btnText">注册</text>
        </button>
      </el-aside>
      <!-- 右侧走马灯 -->
      <el-main style="width: 50%;">
        <my-carousel></my-carousel>
      </el-main>
    </el-container>
  </div>
</template>

<script>
import carousel from './signinCarousel.vue';

export default {
  data() {
    return {
      account: '',
      password: '',
      showPassword: false,
    };
  },
  components: {
      'my-carousel': carousel
  },
  methods: {
    register() {
      // 在这里编写注册逻辑，可以发送请求将账号和密码提交到服务器
      console.log('账号:', this.account);
      console.log('密码:', this.password);
    },
    redirectToRegister() {
      // 跳转到注册页面的逻辑
      this.$router.push('/signup');
    },
    redirectToRecover() {
      // 跳转到忘记密码页面的逻辑
      this.$router.push('/recover');
    },
  }
};
</script>

<style scoped>
.tag {
  cursor: pointer;
  text-decoration: underline;
  margin-top: 50vh;
  margin-left: 31vw;
  color: #7F265B;
 font-family: Nunito Sans;
 font-size: 12px;
 font-style: normal;
 font-weight: 600;
 line-height: normal;
}
.el-main {
  position: absolute;
  background: linear-gradient(180deg, #77B0FE 0%, rgba(119, 176, 254, 0.10) 100%);
  top: 0;
  right: 0;
  width: 61.8%;
  height: 100%;
  padding: 0;
}

/*文字“注册”的style*/
.FontLayout {
  display: flex;
  position: absolute;
  width: 30vw;
  height: 20vh;
  top: 6vh;
  left: -3vw;
  flex-direction: column;
  flex-shrink: 0;
}

/*用来装入每一个子标题+输入框的box*/
.subBox {
  position: absolute;
  left: 3vw;
  width: 25vw;
  height: 5vw;
  flex-shrink: 0;
}

/*每一个输入框的样式及内置提示*/
.inputBox {
  position: relative;
  left:6.5vw;
  display: flex;
  width: 30vw;
  padding: 0.5vh 0.6vw;
  align-items: center;
  gap: 0;
  border-radius: 0.31rem;
  border: 0;
}

/*每一个子标题的位置及样式*/
.inputText{
  left:7.3vw;
  position: relative;
  color: var(--gray-3, #828282);
  font-family: Nunito Sans;
  font-size: 1rem;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

/* 按钮 */
.signupBtn {
  position: absolute;
  width: 10vw;
  height: 2.6vw;
  flex-shrink: 0;
  border-radius: 0.5rem;
  background: #007DFA;
  border: 0;
}

.btnText {
  color: #FFF;
  font-family: Poppins;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

/*右侧左上logo样式 */
.imgR {
  top: 13vh;
  left: 3vw;
  width: 4vw;
  height: 7vh;
  position: relative;
}

/*注册标题 */
.labelH1{
    color: #525252;
    font-family: Nunito Sans;
    font-size: 1.8rem;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}

/*右侧标题位置设置 */
.excenter{
  text-align:center;
  line-height:1;
  position: relative;
  top:13vh;
  left:11.5vw;
}

/*图标后FootGame字体*/
.IconText {
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
.imgBox {
  top: 13vh;
  width: 60vw;
  height: 80vh;
  position: relative;
}

.img1 {
  width: 100%;
  height: 100%;
  object-fit: contain;
}

</style>