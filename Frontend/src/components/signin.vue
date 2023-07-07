<!-- 2154314_郑楷_登录界面 2023.07.07 15:00 v1.0.1
 v1.0.0 画出页面
 v1.0.1 给出版本信息-->

<template>
  <div class="common-layout">
    <el-container>
      <el-aside style="width: 100vh;">
        <!-- 注册字体 -->
        <div class="FontLayout">
          <p class="FontTypography">登录</p>
        </div>
        <!-- 输入内容 -->
        <form @submit.prevent="register">
          <!-- 输入账号 -->
          <div class="subBox" style="top: 30vh;">
            <label for="account" class="subTittle subTittleTypography">账号：</label>
            <el-input type="text" id="account" v-model="account" pattern="[a-zA-Z0-9]+" required maxlength="10"
              class="inputBox inputTypography" placeholder="账号只能由数字和字母组成，且长度不超过10个字符"></el-input>
          </div>
          <!-- 输入密码 -->
          <div class="subBox" style="top:50vh;">
            <label for="password" class="subTittle subTittleTypography">密码：</label>
            <el-input type="password" id="password" v-model="password" pattern="[a-zA-Z0-9]+" required maxlength="15"
              class="inputBox inputTypography" placeholder="密码只能由数字和字母组成，且长度不超过15个字符" show-password></el-input>
          </div>
          <!-- 登入按钮 -->
          <button class="signupBtn" style="left: 7vw; bottom: 15vh;">
            <text class="btnText">登录</text>
          </button>
          
          <!-- 找回密码 -->
          <p class="tag" @click="redirectToRecover">忘记密码？</p>
        </form>
        <!-- 注册按钮 -->
        <button class="signupBtn" style="left: 21vw; bottom: 15vh;" @click="redirectToRegister">
            <text class="btnText">注册</text>
        </button>
      </el-aside>
      <el-main>
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
  color: grey;
  text-decoration: underline;
  margin-top: 60vh;
  margin-left: 30vw;
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
  width: 134px;
  height: 89px;
  top: 10px;
  left: 100px;
  flex-direction: column;
  flex-shrink: 0;
}

.FontTypography {
  color: #3E4772;
  font-family: Poppins;
  font-size: 60px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

/*用来装入每一个子标题+输入框的box*/
.subBox {
  position: absolute;
  left: 100px;
  width: 600px;
  height: 70px;
  flex-shrink: 0;
}

/*每一个子标题的位置及样式*/
.subTittle {
  display: flex;
  position: relative;
  left: 0px;
  width: 100px;
  height: 44.917px;
  flex-direction: column;
  flex-shrink: 0;
  text-align: left;
}

.subTittleTypography {
  color: #000;
  font-family: Poppins;
  font-size: 20px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

/*每一个输入框的样式及内置提示*/
.inputBox {
  border: 0;
  position: absolute;
  left: 0px;
  width: 400px;
  height: 40px;
  flex-shrink: 0;
  text-align: center;
}

.inputTypography {
  border-radius: 30px;
  background: #F1F1F1;
}

/* 注册按钮 */
.signupBtn {
  position: absolute;
  width: 180px;
  height: 35px;
  flex-shrink: 0;
  border-radius: 30px;
  background: #2E375D;
}

.btnText {
  color: #FFF;
  font-family: Poppins;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
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
}</style>
