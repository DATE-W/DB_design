<!-- 2154314_郑楷_个人主页 2023.07.20 02:00 v1.1.0
 v1.0.0 画出页面的基本模板，未填充具体内容，只做了组件定位
 v1.1.0 填充了模版内的具体内容，设置了文字与图片，未编写逻辑-->

<template>
  <el-container>
    <el-header>
      <my-nav></my-nav>
    </el-header>

    <el-container>
      <el-aside>
        <!-- 左侧一列 -->

        <!-- 用户信息与邮箱 -->
        <border-box class="bBox" style="width: 200px;height: 20vh; top: 11vh;left:3vw">

          <!-- 用户名 -->
          <div class="userNameLayout" style="left:4vw;">
            <p class="userNameTypo"> Karrigan</p>
          </div>

          <!-- 跳转到个人信息 -->
          <button class="goinBtn" style="right:1vw;top:2vh">
            <!-- 待写入跳转逻辑 -->

            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"
              style="position:absolute;right:0px;bottom: 0px;">
              <path d="M5 14L11 8L5 2" stroke="#161616" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
            </svg>

          </button>

          <!-- 头像图片 -->

          <!-- 待写入跳转逻辑 -->
          <!-- 图片源需更改 -->
          <el-avatar :src="avatarUrl" style=""></el-avatar>

        </border-box>

        <button class="btn2" style="width: 180px; height: 40px; top: 25vh; left: 3vw;position: relative;"
          :style="isEditSelected ? selectedStyle : ''" @click="showdetail">
          <p2 class="textTypo1">个人主页</p2>
        </button>

        <button class="btn2" style="width: 180px; height: 40px; top: 25vh; left: 3vw;position: relative;"
          :style="isEditSelected ? selectedStyle : ''" @click="showedit">
          <p2 class="textTypo1">资料编辑</p2>
        </button>

        <!-- 退出登录 -->
        <el-button class="logout-button" type="danger" @click="logout">登出</el-button>
      </el-aside>
      <el-divider direction="vertical" />
      <el-main>
        <detail v-if="showPage == 'detail'" @change-page="onPageChange"></detail>
        <favorite v-if="showPage == 'favorite'"></favorite>
        <notification v-if="showPage == 'notification'"></notification>
        <credits v-if="showPage == 'credits'"></credits>
      </el-main>

    </el-container>

  </el-container>
</template>



<script>
import MyNav from './nav.vue';
import pDetail from './personalDetail.vue';
import pFavorite from './personalFavorite.vue';
import pNotification from './personalNotification.vue';
import pCredits from './personalCredits.vue';
import { ElDivider } from 'element-plus'



export default {
  components: {
    'my-nav': MyNav,
    'detail': pDetail,
    'favorite': pFavorite,
    'notification': pNotification,
    'credits': pCredits,
  },
  data() {
    return {
      //自定义一个枚举，用来表示当前显示的页面
      showPage: 'detail',
      selectedStyle: {
        backgroundColor: "#FFDFD6", // 选中时的背景颜色
        fontWeight: "600", // 选中时的字体加粗
        lineHeight: "24px", // 选中时的行高
      },
      avatarUrl: "./src/assets/img/carousel1.png", // 头像url
      likeCnt: 0, // 被点赞数

    };
  },
  methods: {
    showedit() {
      this.$router.push('/personalEdit')
    },
    showdetail() {
      this.showPage = 'detail'
    },
    onPageChange(page) {
      this.showPage = page; // 接收到来自子组件的值，修改showPage
    },
    logout() {
      localStorage.removeItem('token');
      this.$router.push('/');
    }
  }
}

</script>

<style scoped>
.el-main {
  margin-top: 5vh;
  padding: 0px;
}

.el-divider {
  height: 92vh;
}

.logout-button {
  position: absolute;
  width: 200px;
  height: 60px;
  bottom: 11vh;
  left: 3vw;
}


/* 容器框样式 */
.bBox {
  position: absolute;
  flex-shrink: 0;
  border-radius: 32px;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  /* 正式版本用此颜色*/
  background-color: #ffffff;
  /* 方便检测位置*/
  /* background-color: #c3c3c3;  */
}

.cBox {
  position: absolute;
  left: 1rem;
  width: 10.5rem;
  height: 5rem;
  flex-shrink: 0;
  /* 正式版本用此颜色*/
  background-color: #ffffff;
  /* 方便检测位置*/
  /* background-color: #c3c3c3; */
}

.iconBox {
  position: absolute;
  top: 0.8rem;
  left: 2rem;
  width: 4rem;
  height: 4rem;
  flex-shrink: 0;
  /* 仅为检测位置，正式版本删去*/
  /* border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);  */
}

.miniBox {
  position: absolute;
  top: 1rem;
  left: 1rem;
  width: 2rem;
  height: 2rem;
  flex-shrink: 0;
  /* 仅为检测位置，正式版本删去*/
  /* border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);  */
}

.textBox1 {
  position: absolute;
  width: 30rem;
  height: 2rem;
  left: 8rem;
  top: -0.8rem;
}

.textBox2 {
  position: absolute;
  width: 50rem;
  height: 2rem;
  left: 8rem;
  top: 3.2rem;
}

.textBox3 {
  position: absolute;
  width: 10rem;
  height: 2rem;
  left: 5rem;
  top: 0.7rem;
}

.textBox4 {
  position: absolute;
  width: 10rem;
  height: 2rem;
  left: 5rem;
  top: 2.2rem;
}

.textBox5 {
  position: absolute;
  width: 17rem;
  height: 2rem;
  left: 1.2rem;
  top: 3.8rem;
}

/* 按钮样式 */
.btn1 {
  position: absolute;
}

.btn2 {
  position: absolute;
  flex-shrink: 0;
  border-radius: 16px;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  /* 正式版本用此颜色*/
  background-color: #ffffff;
  /* 方便检测位置*/
  /* background-color: #939393;  */
}

/* 文字风格 */
.userNameLayout {
  position: absolute;
  display: flex;
  width: 156px;
  flex-direction: column;
  flex-shrink: 0;
}

.userNameTypo {
  color: var(--colors-text-dark-172239100, #172239);
  font-family: Verdana;
  font-size: 16px;
  font-style: normal;
  font-weight: 600;
  line-height: 24px;
}


.titleLayout {
  position: absolute;
  display: flex;
  width: 940px;
  flex-direction: column;
  flex-shrink: 0;
}

/* 按钮风格 */
.goinBtn {
  position: absolute;
  width: 20px;
  height: 20px;
  flex-shrink: 0;
}

/* 图片风格 */
.avatar {
  width: 48px;
  height: 48px;
  border-radius: 48px;
  margin-top: 0.7vh;
  margin-left: 1vw;
  object-fit: cover;
}
</style>