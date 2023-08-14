<!-- 2154314_郑楷_个人主页 2023.07.20 02:00 v1.1.0
 v1.0.0 画出页面的基本模板，未填充具体内容，只做了组件定位
 v1.1.0 填充了模版内的具体内容，设置了文字与图片，未编写逻辑-->

<template>
  <el-container style="height: 100vh; display: flex; flex-direction: column;">
    <el-header>
      <my-nav></my-nav>
    </el-header>

    <el-container>
      <el-aside>
        <!-- 左侧一列 -->

        <!-- 用户信息与邮箱 -->
        <el-card class="personal-card">
          <!-- 用户名 -->
          <div class="userNameLayout">
            <p class="userNameTypo">{{ userName }}</p>
          </div>

          <!-- 头像图片 -->
          <el-avatar :src="avatarUrl" class="avatar"></el-avatar>
          <!-- 关注、粉丝、点赞数 -->
          <el-card shadow="hover" class="childcard" style="margin-top: 6vh;" @click="showtabs = false">
            <div class="childcardInfo">
              <div>关注</div>
              <div>
                {{ followCnt }}&nbsp;
                <el-icon>
                  <ArrowRightBold />
                </el-icon>
              </div>
            </div>
          </el-card>
          <el-card shadow="hover" class="childcard">
            <div class="childcardInfo">
              <div>粉丝</div>
              <div>
                {{ befollowCnt }}&nbsp;
                <el-icon>
                  <ArrowRightBold />
                </el-icon>
              </div>
            </div>
          </el-card>
          <el-card shadow="hover" class="childcard">
            <div class="childcardInfo">
              <div>点赞</div>
              <div>
                {{ likeCnt }}&nbsp;
                <el-icon>
                  <ArrowRightBold />
                </el-icon>
              </div>
            </div>
          </el-card>

        </el-card>

        <button class="btn2" style="width: 180px; height: 40px; margin-top: 5vh; left: 3vw;position: relative;"
          :style="isEditSelected ? selectedStyle : ''" @click="showedit">
          <p2 class="textTypo1">资料编辑</p2>
        </button>

        <!-- 退出登录 -->
        <el-button class="logout-button" type="danger" @click="logout">登出</el-button>
      </el-aside>
      <el-main>
        <div v-if="showtabs">
          <el-tabs type="border-card" tab-position="top" class="maintabs">
            <el-tab-pane label="我的动态">
              <detail />
            </el-tab-pane>
            <el-tab-pane label="我的帖子">
              <post />
            </el-tab-pane>
            <el-tab-pane label="我的收藏">
              <favorite />
            </el-tab-pane>
            <el-tab-pane label="我的积分">
              <credits />
            </el-tab-pane>
            <el-tab-pane label="我的签到">
              <checkin />
            </el-tab-pane>
            <el-tab-pane label="消息通知">
              <notification />
            </el-tab-pane>
          </el-tabs>
        </div>
        <div v-else>
          <following />
        </div>
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
import pCheckin from './personalCheckin.vue';
import pPost from './personalPost.vue';

import pFollowing from './personalFollowing.vue';
export default {
  components: {
    'my-nav': MyNav,
    'detail': pDetail,
    'favorite': pFavorite,
    'notification': pNotification,
    'credits': pCredits,
    'checkin': pCheckin,
    'post': pPost,
    'following': pFollowing
  },
  data() {
    return {
      showtabs: true,
      selectedStyle: {
        backgroundColor: "#FFDFD6", // 选中时的背景颜色
        fontWeight: "600", // 选中时的字体加粗
        lineHeight: "24px", // 选中时的行高
      },
      avatarUrl: "./src/assets/img/carousel1.png", // 头像url
      userName: "WinWin", // 用户名
      followCnt: 123,       // 关注数
      befollowCnt: 114514,     // 被关注数
      likeCnt: 1919810,         // 被点赞总数

    };
  },
  methods: {
    showedit() {
      this.$router.push('/personalEdit')
    },
    logout() {
      localStorage.removeItem('token');
      this.$router.push('/');
    }
  }
}

</script>

<style scoped>
.personal-card {
  padding: 0px;
  /* 调整卡片的内边距 */
  margin: 10px;
  /* 调整卡片的外边距 */
  border: 1px solid #EAEAEA;
  /* 添加卡片的灰色边框 */
  border-radius: 8px;
  /* 设置卡片的圆角 */

  height: 400px;
}

.childcard {
  margin-top: 10px;
  font-size: 20px;
}

.childcardInfo {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
}

.el-main {
  flex: 1;
  padding: 0;
}

/* 修改 el-tabs 样式，设置高度为 100% */
.maintabs {
  border: 0;
  border-left: 1px solid #EAEAEA;
  height: 100%;
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

/* 按钮样式 */
.btn2 {
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
  width: 5vw;
  left: 7vw;
  flex-direction: column;
  flex-shrink: 0;
}

.userNameTypo {
  color: var(--colors-text-dark-172239100, #172239);
  font-family: Verdana;
  font-size: 22px;
  font-style: normal;
  font-weight: 600;
  line-height: 24px;
}


/* 图片风格 */
.avatar {
  width: 50px;
  height: 50px;
  border-radius: 48px;
  margin-top: 0.7vh;
  margin-left: 1vw;
  object-fit: cover;
}
</style>