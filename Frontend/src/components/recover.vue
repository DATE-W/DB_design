<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="50%" class="leftBg">
        <br>
          <div>
            <img src="../assets/img/logo.png" class="leftLogo1">
            <p class="leftLogo2">FootGame</p>
         </div>
          
      </el-aside>
      <el-main>
        <div class="right">
          <img src="../assets/img/recover_logo.png" class="imgR">
        <div class="mainbox">
          
          <el-space direction="vertical">
<<<<<<< Updated upstream
            <h1 style="color: #525252;font-family: Nunito Sans;font-size: 30px;font-style: normal;font-weight: 700;line-height: normal;">Reset password to retrieve it</h1>    
=======
            <h1 class="labelH">Reset password to retrieve it</h1>    
>>>>>>> Stashed changes
            <el-text tag="i" style="font-size: 15px;">Answer security questions and retrieve password</el-text>
          </el-space>
          <div>
           <label for="securityAns" class="inputtext" style="left: -182px;">密保问题：&nbsp;&nbsp;&nbsp;</label>
           <p >{{ securityQ }}</p>
<<<<<<< Updated upstream
           <label for="securityAns" class="inputtext">密保答案：&nbsp;&nbsp;&nbsp;</label>
           <input type="text" id="securityAns" v-model="securityAns" required  class="inputbox">

           <br>
              <label for="newPword" class="inputtext">输入新密码：</label>
              <input  v-model="newPword" :type="passwordVisible ? 'text' : 'password'" class="inputbox">
            <br>
              <label for="confirmPword" class="inputtext">确认新密码：</label>
=======
           <label for="securityAns" class="inputtext" style="left: -182px;">密保答案：&nbsp;&nbsp;&nbsp;</label>
           <input type="text" id="securityAns" v-model="securityAns" required  class="inputbox">

           <br>
              <label for="newPword" class="inputtext" style="left: -180px;">输入新密码：</label>
              <input  v-model="newPword" :type="passwordVisible ? 'text' : 'password'" class="inputbox">
            <br>
              <label for="confirmPword" class="inputtext" style="left: -180px;">确认新密码：</label>
>>>>>>> Stashed changes
              <input id="confirmPword" v-model="confirmPword" :type="passwordVisible ? 'text' : 'password'" required  class="inputbox">
              <el-text tag="i" class="labeltext">Please remember your password!</el-text>
            <br>&nbsp;
            <br>&nbsp;
              <button @click="ShoworHide">{{ passwordVisible ? '隐藏密码' : '显示密码' }}</button>
            <br>
              <button @click="recoverPword" class="button">确认修改</button>
          </div>
          
        </div>

        </div>
        
      </el-main>
    </el-container>
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

<style scoped>
<<<<<<< Updated upstream
=======
.labelH1{
  color: #525252;font-family: Nunito Sans;font-size: 30px;font-style: normal;font-weight: 700;line-height: normal;
}

>>>>>>> Stashed changes
  .common-layout {
    height: 100vh; /* 设置容器高度为视口高度 */
    width: 100%; /* 设置容器宽度为100% */
  }

  .el-container {
    height: 100%; /* 设置 el-container 元素占满容器高度 */
    width: 100%; /* 设置 el-container 元素占满容器宽度 */
  }

  .leftBg{
    /* background: linear-gradient(180deg, #77B0FE 0%, rgba(119, 176, 254, 0.10) 100%); */
    background: linear-gradient(180deg, rgba(49, 49, 49, 0.30) 0%, rgba(0, 0, 0, 0.55) 100%), url(../assets/img/recover.png), lightgray 50% / cover no-repeat;
  }
  .leftLogo1{
    top: 120px;
    left:50px;
    width: 60px;
    height: 60px;
    flex-shrink: 0;
    fill: #ffffff;
    position: relative;
  }
  .leftLogo2{
    color: #ffffff;
    font-family: Poppins;
    font-size: 50px;
    font-style: normal;
    font-weight: 600;
    line-height: 163%;
    letter-spacing: 10px;
    position: relative;
    left:160px;
  }

  .imgR{
    top:80px;
    left: -250px;
    
    position: relative;
  }

  .right{
    text-align:center;
    vertical-align: middle;
    line-height:1;
  }

  .mainbox{
    top:130px;
    left:200px;
    position: relative;
    display: flex;
    width: 450px;
    height: 450px;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 36px;
    flex-shrink: 0;  
    /*background-color: antiquewhite;*/
  }

  .inputtext{
    position: relative;
    color: var(--gray-3, #828282);
    font-family: Nunito Sans;
    font-size: 14px;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
<<<<<<< Updated upstream
    left: -180px;
=======
>>>>>>> Stashed changes
    bottom: 5px;
  }

  .inputbox{
    position: relative;
    display: flex;
    width: 420px;
    padding: 13px 10px;
    align-items: center;
    gap: 13px;
    border-radius: 5px;
    border: 1px solid #DED2D9;
  }

  .button{
    display: flex;
    width: 420px;
    padding: 13px 10px 12px 10px;
    justify-content: center;
    align-items: center;
    gap: 13px;
    border-radius: 6px;
    background: #007DFA;
    color: #FFF;
    font-family: Nunito Sans;
    font-size: 18px;
    font-style: normal;
    font-weight: 800;
    line-height: normal;
    border: 0;
  }

  .labeltext{
    color: #7F265B;
    font-size: 10px;
    position: relative;
    left:130px;
    top:0px;
    bottom:10px;
  }

</style>
