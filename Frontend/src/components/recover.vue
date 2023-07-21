<template>
  <div class="common-layout">
    <el-container>
      <!-- 左侧走马灯 -->
      <el-aside width="50%" class="leftBg">
        <my-carousel></my-carousel>
      </el-aside>
      <!-- 右侧输入框 -->
      <el-main>

        <div class="right">
          <!-- 右侧左上角logo -->
          <img src="../assets/img/recover_logo.png" class="imgR">
          <!-- 右侧总框夹 -->
          <div class="mainbox">
            <!-- 找回密码标题 -->
            <el-space direction="vertical">
              <h1 class="labelH1">Reset password to retrieve it</h1>
              <el-text tag="i" style="font-size: 0.9rem;">Answer security questions and retrieve password</el-text>
            </el-space>
            <div>
              <!-- 密保问题 -->
              <label for="securityAns" class="inputtext" style="left: -13vw;">密保问题：&nbsp;&nbsp;&nbsp;</label>
              <p>{{ securityQ }}</p>
              <!-- 密保答案 -->
              <label for="securityAns" class="inputtext" style="left: -13vw;">密保答案：&nbsp;&nbsp;&nbsp;</label>
              <el-input type="text" id="securityAns" v-model="securityAns" required class="inputbox" />
              <br>
              <!-- 新密码 -->
              <label for="newPword" class="inputtext" style="left: -12.9vw;">输入新密码：</label>
              <el-input type="password" id="password" v-model="newPword" pattern="[a-zA-Z0-9]+" required maxlength="15"
                class="inputbox" show-password placeholder="密码只能由数字和字母组成，且长度不超过15个字符" />
              <br>
              <!-- 确认密码 -->
              <label for="confirmPword" class="inputtext" style="left: -12.9vw;">确认新密码：</label>
              <!-- <el-input id="confirmPword" v-model="confirmPword" show-password required class="inputbox" /> -->
              <el-input type="password" id="confirmPword" v-model="confirmPword" pattern="[a-zA-Z0-9]+" required
                maxlength="15" class="inputbox" show-password placeholder="密码只能由数字和字母组成，且长度不超过15个字符" />
              <!-- 温馨提示 -->
              <el-text tag="i" class="labeltext">Please remember your password!</el-text>
              <br>&nbsp;
              <br>
              <!-- 确认按钮 -->
              <button @click="recoverPword" class="button">确认修改</button>
            </div>

          </div>

        </div>

      </el-main>
    </el-container>
  </div>
</template>


<script>
import carousel from './signinCarousel.vue';
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
  components: {
    'my-carousel': carousel
  },
  methods: {
    ShoworHide() {
      this.passwordVisible = !this.passwordVisible;
      console.log(passwordVisible);
    },
    recoverPword() {
      if (this.newPword !== this.confirmPword) {
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

<style scoped>
/*注册标题*/
.labelH1 {
  color: #525252;
  font-family: Nunito Sans;
  font-size: 1.8rem;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

.common-layout {
  height: 100vh;
  /* 设置容器高度为视口高度 */
  width: 100%;
  /* 设置容器宽度为100% */
}

.el-container {
  height: 100%;
  /* 设置 el-container 元素占满容器高度 */
  width: 100%;
  /* 设置 el-container 元素占满容器宽度 */
}

/*左侧背景图片 */
.leftBg {
  /* background: linear-gradient(180deg, #77B0FE 0%, rgba(119, 176, 254, 0.10) 100%); */
  background: linear-gradient(180deg, rgba(49, 49, 49, 0.30) 0%, rgba(0, 0, 0, 0.55) 100%), url(../assets/img/recover.png), lightgray 50% / cover no-repeat;
}

/*右侧左上logo样式 */
.imgR {
  top: 8vh;
  left: -16vw;
  width: 4vw;
  height: 7vh;
  position: relative;
}

/*右侧总览 */
.right {
  text-align: center;
  vertical-align: middle;
  line-height: 1;
}

/*右侧输入总框 */
.mainbox {
  top: 16vh;
  left: 11vw;
  position: relative;
  display: flex;
  width: 30vw;
  height: 45vh;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 2.21rem;
  flex-shrink: 0;
  /*background-color: antiquewhite;*/
}

/*输入提示标题 */
.inputtext {
  position: relative;
  color: var(--gray-3, #828282);
  font-family: Nunito Sans;
  font-size: 0.9rem;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  bottom: 1vh;
  top: 0.5vh;
}

/*输入框 */
.inputbox {
  position: relative;
  display: flex;
  width: 33vw;
  padding: 1.3vh 0.6vw;
  align-items: center;
  gap: 0.78rem;
  border-radius: 0.31rem;
  border: 0;
  bottom: 2vh;
  top: 0.5vh;
}

/*确认按钮 */
.button {
  position: relative;
  top: 3vh;
  left: 0.5vw;
  display: flex;
  width: 32vw;
  padding: 1.3vh 0.6vw 1.2vh 0.6vw;
  justify-content: center;
  gap: 0.78rem;
  border-radius: 0.31rem;
  background: #007DFA;
  /*文字设置*/
  color: #FFF;
  font-family: Nunito Sans;
  font-size: 1.1rem;
  font-style: normal;
  font-weight: 800;
  line-height: normal;
  border: 0;

}

/*温馨提示样式*/
.labeltext {
  color: #7F265B;
  font-size: 0.6rem;
  position: relative;
  left: 10vw;
  top: 0;
  bottom: 1vh;
}
</style>
