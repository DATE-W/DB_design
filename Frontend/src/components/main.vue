<template>
  <div>
    <my-nav></my-nav>
    <el-main >
      <div class="main-container">
          <!-- Scroll to Top Button -->
          <div @click="scrollToTop" class="scroll-to-top-btn">
            <div class="image-container">
              <img src="../assets/img/ToTop.png" alt="Scroll to Top">
            </div>
            <div class="text-overlay">
              <div class="line">{{ line1 }}</div>
              <div class="line">{{ line2 }}</div>
            </div>
          </div>
          <!-- Carousel -->
          <el-carousel style="height:80vh;z-index: 1;width:100%;" arrow="never">
            <!-- Images to display above the carousel -->
            <div style="position: absolute; top: 16%; left: 0; display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;">
              <img src="../assets/img/home_slogan1.png" style="max-width: 1000px; margin: 10px; z-index: 2;">
            </div> 
            <div style="position: absolute; top: 67%; left: 0; display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;">
              <img src="../assets/img/home_slogan2.png" style="max-width: 1000px; margin: 10px; z-index: 2;">
            </div> 
            <!-- Replace the v-for loop with your custom carousel items -->
            <el-carousel-item v-for="item in 3" :key="item" style="width: 100%;height: 80vh;display: flex; justify-content: center; align-items: center;">
              <img :src="getImageUrl(item)" alt="Carousel Image" class="carousel-image" style="border: 0width: 100%;object-fit: cover;"/>
            </el-carousel-item>
          </el-carousel>
        <div class="top-section">
          <!-- 赛事信息筛选菜单 -->
          <el-menu class="Games-menu" mode="horizontal" @select="handleMenuSelect">
            <div class="menu-wrapper">
              <el-menu-item class="menu-item" index="1" @click="GamesAll">
                <div class="menu-item-content">
                  <p class="menu-title">全部</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="2" @click="GamesZC">
                <div class="menu-item-content">
                  <p class="menu-title">中超</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="3" @click="GamesYC">
                <div class="menu-item-content">
                  <p class="menu-title">英超</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="4" @click="GamesYJ">
                <div class="menu-item-content">
                  <p class="menu-title">意甲</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="5" @click="GamesXJ">
                <div class="menu-item-content">
                  <p class="menu-title">西甲</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="6" @click="GamesDJ">
                <div class="menu-item-content">
                  <p class="menu-title">德甲</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="7" @click="GamesFJ">
                <div class="menu-item-content">
                  <p class="menu-title">法甲</p>
                </div>
              </el-menu-item>
              <el-menu-item class="menu-item" index="8" @click="redirectToGames">
                <div class="menu-item-content">
                  <p class="menu-title">更多</p>
                </div>
              </el-menu-item>
            </div>
          </el-menu>


          <!-- 赛事信息卡片 -->
          <el-row class="Game-col-container">
            <el-card shadow="hover" class="Game-card" v-for="item in getLimitedGames()" :key=item.index
            style="border-radius: 10px; border: none; margin: 5px;">
              <div class="Game-content">
                <div class="column-status">{{ item.status }}</div>
                <div class="column-team1">{{ item.team1 }} {{ item.score1 }}</div>
                <div class="column-team1">{{ item.team2 }} {{ item.score2 }}</div>
                <el-button class="button" text>详细赛事信息</el-button>
              </div>
            </el-card>
          </el-row>
        </div>
        <!-- 下中半部分 -->
        <div class="bottom-middle-section">
          <!-- 左侧新闻板块 -->
          <div class="news-container">
            <div class="hot-news">热点资讯</div>
            <div v-for="(news, index) in getLimitedNews()" :key="index" class="news-item">
              <a :href="news.link" target="_blank" class="news-link">
                <el-row align="middle" class="news-row" @click="goToLink(news.link)">
                  <el-col :span="24" style="display: flex; align-items: center;">
                    <!-- Image -->
                    <div class="news-item-wrapper">
                      <img :src="news.image" alt="News Image" class="news-image">
                      <!-- News Content -->
                      <div class="news-content">
                        <h4 class="news-title">{{ news.title }}</h4>
                        <p class="news-summary">{{ truncateText(news.summary, 50) }}</p>
                      </div>
                    </div>
                  </el-col>
                </el-row>
              </a>
            </div>
          </div>

          <!-- 右侧社区板块 -->
          <div class="forum-container">
            <div v-for="(posts, index) in getLimitedPosts()" :key="index" class="posts-item">
              <a :href="posts.link" target="_blank" class="posts-link">
                <el-row align="middle" class="posts-row" @click="goToLink(posts.link)">
                  <el-col :span="24" style="display: flex; align-items: center;">
                    <!-- Image -->
                    <div class="posts-item-wrapper">
                      <img :src="posts.image" alt="Posts Image" class="posts-image">
                      <!-- News Content -->
                      <div class="posts-content">
                        <h4 class="posts-title">{{ posts.title }}</h4>
                        <p class="posts-summary">{{ truncateText(posts.summary, 50) }}</p>
                      </div>
                    </div>
                  </el-col>
                </el-row>
              </a>
            </div>
          </div>
        </div>

        <!-- 下半部分 -->
        <div class="bottom-section">
            <div class="module" @click="redirectToNews" @mouseover="hideContent" @mouseout="showContent">
                <p class="module-text">致力于分享最有价值的新闻</p>
                <div class="circle">N</div>
                <div class="overlay-background">
                  <p class="overlay-text">一览足球热讯<br>->新闻</p>
                </div>
            </div>

            <div class="module" @click="redirectToGames" @mouseover="hideContent" @mouseout="showContent">
                <p class="module-text">一场场足球的视觉盛宴</p>
                <div class="circle">E</div>
                <div class="overlay-background">
                  <p class="overlay-text">享受足球魅力<br>->赛事</p>
                </div>
            </div>

            <div class="module" @click="redirectToForum" @mouseover="hideContent" @mouseout="showContent">
                <p class="module-text">一个充满热情的思想空间</p>
                <div class="circle">F</div>
                <div class="overlay-background">
                  <p class="overlay-text">表达真挚热爱<br>->论坛</p>
                </div>
            </div>
        </div>
      </div>
    </el-main>
  </div>
</template>
  
<script>
import MyNav from './nav.vue';
import carousel1 from '../assets/img/home_slider1.jpg';
import carousel2 from '../assets/img/home_slider2.jpg';
import carousel3 from '../assets/img/home_slider3.jpg';

export default {
  data() {
    return {
      line1: '回到',
      line2: '顶部',
      newsList: [
        {
          title: "王晗天天拉屎",
          summary: "有消息人士称301寝室的wh一天能拉三泡屎，请跟随小编一起来看看吧",
          image: "../src/assets/img/carousel1.png",
          link: "https://sse.tongji.edu.cn/"
        },
        {
          title: "lll今天又睡过头了",
          summary: "猪王lll今天无故缺席，斩立决",
          image: "../src/assets/img/carousel2.png",
          link: "https://sse.tongji.edu.cn/"
        },
        {
          title: "wyh好帅",
          summary: "wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅",
          image: "../src/assets/img/carousel3.png",
          link: "https://sse.tongji.edu.cn/"
        }
      ],
      postsList: [
        {
          title: "王晗天天拉屎",
          summary: "有消息人士称301寝室的wh一天能拉三泡屎，请跟随小编一起来看看吧",
          image: "../src/assets/img/carousel1.png",
          link: "https://sse.tongji.edu.cn/"
        },
        {
          title: "lll今天又睡过头了",
          summary: "猪王lll今天无故缺席，斩立决",
          image: "../src/assets/img/carousel2.png",
          link: "https://sse.tongji.edu.cn/"
        },
        {
          title: "wyh好帅",
          summary: "wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅",
          image: "../src/assets/img/carousel3.png",
          link: "https://sse.tongji.edu.cn/"
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
      maxGamesItems: 8,
      maxNewsItems: 3,
      maxPostsItems: 3,
      maxNewsLength: 30,
      GameSelect: "ALL"
    }
  },
  components: {
    'my-nav': MyNav
  },
  methods: {
    scrollToTop() {
      // Scroll to top logic
      window.scrollTo({
        top: 0,
        behavior: "smooth" // Add smooth scrolling animation
      });
    },
    redirectToForum() {
      //跳转到论坛页面的逻辑
      this.$router.push('/forum')
    },
    redirectToGames(){
      //跳转到赛事页面的逻辑
      this.$router.push('/Games')
    },
    redirectToNews(){
      //跳转到新闻页面的逻辑
      this.$router.push('/News')
    },
    getImageUrl(item) {
        switch (item) {
          case 1:
            return carousel1;
          case 2:
            return carousel2;
          case 3:
            return carousel3;
          default:
            return '';
        }
    },

    getLimitedGames() {
      return this.GamesMsg.filter(item => item.Game === this.GameSelect || this.GameSelect === "ALL").slice(0, this.maxGamesItems);
    },
    getLimitedNews() {
      return this.newsList.slice(0, this.maxNewsItems);
    },
    getLimitedPosts() {
      return this.postsList.slice(0, this.maxPostsItems);
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
.main-container{
  background-color: #a8e3f3;
}

.top-section {
  background-color: #f0f0f0;
}
/* 赛事种类选择栏 */
.Games-menu {
  /* border-radius: 10px;
  padding: 10px; */
  background-color: #4dcdf0;
}

.el-menu-item {
  border: none;
  background-color: transparent;
  cursor: pointer;
  transition: transform 0.2s ease;
}

.el-menu-item:hover {
  transform: scale(1.05);
}

.menu-wrapper {
  display: flex;
  align-items: center;
  justify-content: center; /* 将菜单项居中显示 */
  max-width: 600px; /* 限定父容器的最大宽度 */
  margin: 0 auto; /* 居中显示父容器 */
}

.menu-title {
  font-size: 16px;
  color: #333;
  margin: 0;
  padding: 10px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.menu-title:hover {
  color: #4dcdf0;
}

/* 赛事信息卡片 */
.Game-col-container {
  background-color: #4dcdf0;
  display: flex;
  flex-wrap: nowrap;
  padding-top: 10px;
  padding-bottom: 10px;
  /* 防止列折行 */
}

.Game-col {
  flex: 0 0 20%;
  /* 一行显示六个列，每个列占比16.66% */
  padding: 10px;
}


.bottom-middle-section {
  display: flex;
  flex: 2;
  padding-top: 10px;
  justify-content: space-between;
}
/* 论坛模块 */
/* Center the forum container and set some spacing */
.forum-container {
  width:40%;
  /* display: flex; */
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  flex-wrap: wrap;
  justify-content: center;
  gap: 20px;
  margin-top: 20px;
}

.posts-item {
  width: 100%;
  background-color: #f9f9f9;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  transition: transform 0.2s ease;
}

.posts-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.posts-image {
  width: 100px;
  height: 100px;
  object-fit: cover;
  margin-right: 10px;
  border-radius: 50%; /* Add this line to make the element circular */
}

.posts-item-wrapper {
  display: flex;
  align-items: center;
  width: 100%;
  margin: 0; /* Remove default margin */
  padding: 0; /* Remove default padding */
}

.posts-content {
  padding: 10px;
  max-width: 400px;
  flex: 1; /* Make the content take the remaining width */
}

.posts-item-wrapper:hover {
  background-color: #f2f2f2; /* Light gray background color on hover */
}

.posts-link {
  display: block;
  text-decoration: none;
  color: inherit;
  color: #333;
}

.posts-row {
  padding: 10px;
}

.posts-title {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 5px;
}

.posts-summary {
  font-size: 14px;
  color: #666;
}
/* 新闻模块 */
.news-container{
  width: 60%;
}
.hot-news {
  font-size: 36px;
  font-weight: bold;
  color: #ff4500;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
}

.news-item {
  margin-bottom: 20px; 
}

.news-image {
  width: 150px;
  height: 100px;
  object-fit: cover;
  margin-right: 10px;
}

.news-item-wrapper {
  display: flex;
  align-items: center;
  width: 100%;
  margin: 0; 
  padding: 0; 
}

.news-content {
  max-width: 400px;
  flex: 1; 
}

.news-item-wrapper:hover {
  background-color: #f2f2f2; 
}

.news-link {
  display: block;
  text-decoration: none;
  color: inherit;
}


/* 轮播图 */
.carousel-image {
  width: 1800px;
  object-fit: contain;
}

.carousel-item-container {
  width: 100%;
  height: 80vh;
  display: flex;
  justify-content: center;
  align-items: center;
  box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.3); /* Add the box-shadow here */
}

.carousel-image {
  border: 0;
  width: 1800px;
  object-fit: contain;
}

/* 右下组件——回到顶部 */
.scroll-to-top-btn {
  position: fixed;
  bottom: 20px;
  right: 20px;
  width: 50px; /* Set the desired width for the square image */
  height: 50px; /* Set the desired height for the square image */
  cursor: pointer;
  overflow: hidden; /* Hide overflowing content */
  z-index: 999;
}

.image-container img {
  width: 100%;
  height: 100%;
  display: block;
  transition: opacity 0.3s;
}

.text-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  opacity: 0; /* Hide the overlay by default */
  pointer-events: none; /* Prevent overlay from blocking clicks */
  transition: opacity 0.3s;
}

.scroll-to-top-btn:hover .image-container img {
  opacity: 0; /* Hide the image on hover */
}

.scroll-to-top-btn:hover .text-overlay {
  opacity: 1; /* Show the overlay on hover */
}

.line {
  color: #4dcdf0;
  font-size: 12px;
}

/* <!-- 下半部分 --> */
/* 赛事，论坛，新闻选择 */
.bottom-section {
    display: flex;
    justify-content: space-around;
    background-color: #f0f0f0;
    padding: 20px;
}

/* 设置模块样式 */
.module {
    position: relative;
    text-align: center;
    padding: 20px;
    background-color: #fff;
    border: 1px solid #ccc;
    border-radius: 10px;
    width: 30%;
    cursor: pointer; /* 添加交互：将鼠标光标变为手型 */
    transition: transform 0.3s ease;
}

.module:hover {
    transform: scale(1.05);
}

.module .circle {
    width: 80px;
    height: 80px;
    line-height: 80px;
    border-radius: 50%;
    background-color: #007bff;
    color: #fff;
    font-size: 36px;
    margin: 0 auto 10px;
}

.module .module-text {
    font-size: 16px;
    color: #777;
}

.overlay-background {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 123, 255, 0.7);
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 10px;
    opacity: 0;
    transition: opacity 0.3s ease;
}

.module:hover .overlay-background {
    opacity: 1;
}

.module .overlay-text {
    font-size: 24px;
    color: #fff;
}

.module .module-text, .module .circle {
    visibility: visible;
}

.module:hover .module-text, .module:hover .circle {
    visibility: hidden;
}
</style>


<!-- <button @click="this.$router.push('/signin')">登录</button>
<button @click="this.$router.push('/signup')">注册</button> -->
