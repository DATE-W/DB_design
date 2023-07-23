<template>
  <div>
    <my-nav></my-nav>
    <el-main>
      <div class="main-container">
        <div class="top-section">
          <!-- 赛事信息筛选菜单 -->
          <el-menu class="Games-menu" mode="horizontal" active-text-color="#409eff" @select="handleMenuSelect">
            <el-menu-item index="1" @click="GamesAll">全部</el-menu-item>
            <el-menu-item index="2" @click="GamesZC">中超</el-menu-item>
            <el-menu-item index="3" @click="GamesYC">英超</el-menu-item>
            <el-menu-item index="4" @click="GamesYJ">意甲</el-menu-item>
            <el-menu-item index="5" @click="GamesXJ">西甲</el-menu-item>
            <el-menu-item index="6" @click="GamesDJ"> 德甲</el-menu-item>
            <el-menu-item index="7" @click="GamesFJ">法甲</el-menu-item>
            <el-menu-item index="8" @click="redirectToGames">更多赛事信息</el-menu-item>
          </el-menu>
          <!-- 赛事信息卡片 -->
          <el-row class="Game-col-container">
            <el-card shadow="hover" class="Game-card" v-for="item in getLimitedGames()" :key=item.index>
              <!-- <template #header>
            <div class="card-header">
            <span>Card name</span>
            <el-button class="button" text>更多</el-button>
            </div>
          </template> -->
              <div class="Game-content">
                <div class="column-status">{{ item.status }}</div>
                <div class="column-team1">{{ item.team1 }} {{ item.score1 }}</div>
                <div class="column-team1">{{ item.team2 }} {{ item.score2 }}</div>
                <el-button class="button" text>详细赛事信息</el-button>
              </div>
            </el-card>
          </el-row>
        </div>
        <!-- 下半部分 -->
        <div class="bottom-section">
          <!-- 左侧新闻板块 -->
          <div class="news-container">
            <div v-for="(news, index) in getLimitedNews()" :key="index" class="news-item">
              <el-row>
                <el-col :span=6>
                  <img :src="news.image" alt="News Image" class="news-image"></el-col>
                <el-col :span=18>
                  <div class="news-content">
                    <h3 class="news-title">{{ news.title }}</h3>
                    <p class="news-summary">{{ truncateText(news.summary, 50) }}</p>
                  </div>
                </el-col>
              </el-row>
            </div>
          </div>
          <!-- 右侧社区板块 -->
          <div class="forum-container">右侧</div>
        </div>
      </div>
    </el-main>
  </div>
</template>
  
<script>
import MyNav from './nav.vue';

export default {
  data() {
    return {
      newsList: [
        {
          title: "王晗天天拉屎",
          summary: "有消息人士称301寝室的wh一天能拉三泡屎，请跟随小编一起来看看吧",
          image: "../src/assets/img/carousel1.png"
        },
        {
          title: "lll今天又睡过头了",
          summary: "猪王lll今天无故缺席，斩立决",
          image: "../src/assets/img/carousel2.png"
        },
        {
          title: "wyh好帅",
          summary: "wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅",
          image: "../src/assets/img/carousel3.png"
        }
      ],
      GamesMsg: [
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "英超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "德甲", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "意甲", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "法甲", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "法甲", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "法甲", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 },
        { Game: "中超", time: "", status: "已结束", team1: "中国", score1: 74, team2: "澳大利亚", score2: 60 }
      ],
      maxGamesItems: 9,
      maxNewsItems: 3,
      maxNewsLength: 30,
      GameSelect: "ALL"
    }
  },
  components: {
    'my-nav': MyNav
  },
  methods: {
    getLimitedGames() {
      // return this.GamesMsg.slice(0, this.maxItems);
      return this.GamesMsg.filter(item => item.Game === this.GameSelect || this.GameSelect === "ALL").slice(0, this.maxGamesItems);
    },
    getLimitedNews() {
      return this.newsList.slice(0, this.maxNewsItems);
    },
    truncateText(text, limit) {
      if (text.length <= limit) {
        return text;
      } else {
        const truncatedText = text.slice(0, limit);
        return truncatedText + '...';
      }
    },
    GamesAll() {
      //选择全部赛事
      this.GameSelect = "ALL";
    },
    GamesZC() {
      //选择中超赛事
      this.GameSelect = "中超";
    },
    GamesYC() {
      //选择英超赛事
      this.GameSelect = "英超";
    },
    GamesYJ() {
      //选择意甲赛事
      this.GameSelect = "意甲";
    },
    GamesXJ() {
      //选择西甲赛事
      this.GameSelect = "西甲";
    },
    GamesDJ() {
      //选择德甲赛事
      this.GameSelect = "德甲";
    },
    GamesFJ() {
      //选择法甲赛事
      this.GameSelect = "法甲";
    },
    redirectToGames() {
      //跳转到个人中心页面的逻辑
      this.$router.push('/Games')
    }
  },
};
</script>

<style>
.main-container {
  display: flex;
  flex-direction: column;
  height: 100%;
  position: relative;
}

.Games-menu {
  font-size: 16px;
}

.top-section {
  flex: 1;
  background-color: #f0f0f0;
}

.Game-col-container {
  display: flex;
  flex-wrap: nowrap;
  /* 防止列折行 */
}

.bottom-section {
  display: flex;
  flex: 2;
}

.Game-col {
  flex: 0 0 20%;
  /* 一行显示六个列，每个列占比16.66% */
  padding: 10px;
}

.news-container,
.forum-container {
  flex: 1;
  background-color: #d0d0d0;
}

.news-image {
  width: 150px;
  /* 设置图片宽度 */
  height: 150px;
  /* 设置图片高度 */
  object-fit: cover;
  margin-right: 10px;
}
</style>


<!-- <button @click="this.$router.push('/signin')">登录</button>
<button @click="this.$router.push('/signup')">注册</button> -->
