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
          <div style="display: flex; justify-content: center; align-items: center; height: 76vh; width: 100vw;">
            <el-carousel style="height: 76vh; z-index: 1; width: 86vw; margin: auto; position: relative;" arrow="never">
            <!-- Images to display above the carousel -->
            <div style="position: absolute; top: 5%; left: 0; display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;">
              <img src="../assets/img/home_slogan1.png" style="max-width: 600px; width: 50%; margin: 10px; z-index: 2;">
            </div> 
            <div style="position: absolute; top: 55%; left: 0; display: flex; justify-content: center; align-items: center; width: 100%; height: 100%;">
              <img src="../assets/img/home_slogan2.png" style="max-width: 600px; width: 50%; margin: 10px; z-index: 2;">
            </div> 
            <!-- Replace the v-for loop with your custom carousel items -->
            <el-carousel-item v-for="item in 3" :key="item" style="width: 86vw; height: 76vh; display: flex; justify-content: center; align-items: center;">
              <img :src="getImageUrl(item)" alt="Carousel Image" class="carousel-image" style="border: 0; width: 100%; height: 90%; object-fit: cover;" />
            </el-carousel-item>
          </el-carousel>
          </div>
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
            style="border-radius: 10px; border: none; margin: 5px;background-color: #d7ecffca;">
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
            <div class="news-row">
              <div v-for="(news, index) in getLimitedNews()" :key="index" 
                class="news-item" :class="{ 'two-columns': (index + 2) % 5 === 0 }">
                <a :href="news.link" target="_blank" class="news-link">
                  <el-row align="middle" @click="goToLink(news.link)">
                    <el-col :span="24" style="display: flex; align-items: center;">
                      <!-- Image -->
                      <div class="news-item-wrapper" style="position: relative;">
                        <img :src="news.image" alt="News Image" class="news-image">
                        <!-- News Content -->
                        <div class="news-content-overlay" v-if="(index + 2) % 5 === 0"
                            style="position: absolute; top: 70%; left: 0; width: 100%; text-align: center; color: white; background-color: rgba(0, 0, 0, 0.281); padding: 10px;">
                          <h4 class="news-title">{{ news.title }}</h4>
                          <p class="news-summary">{{ truncateText(news.summary, 26) }}</p>
                        </div>
                        <div v-else class="news-content" style="text-align: center;">
                          <h4 class="news-title">{{ news.title }}</h4>
                          <p class="news-summary">{{ truncateText(news.summary, 26) }}</p>
                        </div>
                      </div>
                    </el-col>
                  </el-row>
                </a>
              </div>
            </div>
          </div>

           <!-- 添加一个缝隙 -->
          <div class="spacer" style="width: 30px;"></div>

          <!-- 右侧社区板块 -->
          <div class="forum-container">
            <div class="hot-posts">精选热帖</div>
            <div class="posts-column">
              <div v-for="(post, index) in post_id" :key="post.id" class="posts-item">
                <el-row align="middle" class="posts-row" @click="goToLink(post.link)">
                  <el-col :span="24" style="display: flex; align-items: center;height: 100px;">
                    <div class="posts-item-wrapper">
                      <!-- News Content -->
                      <!-- <a :href="post.link" target="_blank" class="posts-link"> -->
                        <div class="posts-content" >
                          <p :class="['posts-summary', { 'hovered-summary': hoveredIndex === index }]"
                            @mouseenter="hoveredIndex = index"
                            @mouseleave="hoveredIndex = -1">
                            <span :class="['post-number', getColorClass(index)]">
                              {{ index + 1 }}.
                            </span>
                            {{ truncateText(post_content[index] , 50) }}
                          </p>
                          <div class="info-group">
                            <span class="like-container">
                              <el-icon size="medium"><Pointer /></el-icon>
                              <span class="like-number">{{ post_likes[index] }}</span>
                            </span>
                            <span class="space-between"></span> <!-- Add a spacer with desired gap -->
                            <span class="star-container">
                              <el-icon size="medium"><Star /></el-icon>
                              <span class="star-number">{{ post_stars[index] }}</span>
                            </span>
                          </div>
                        </div>
                      <!-- </a> -->
                    </div>
                  </el-col>
                </el-row>
              </div>
            </div>
          </div>
        </div>

        <!-- 下半部分 -->
        <div class="bottom-section">
          <div class="module" @click="redirectToNews" @mouseover="hideContent" @mouseout="showContent">
              <p class="module-text">致 力 于 分 享<br>最 有 价 值 的 新闻</p>
              <el-icon class="news-icon" ><VideoCamera /></el-icon>
              <div class="overlay-background">
                <p class="overlay-text">一览足球热讯<br>->新闻</p>
              </div>
          </div>

          <div class="module" @click="redirectToGames" @mouseover="hideContent" @mouseout="showContent">
              <p class="module-text">一 场 场 足 球<br>视 觉 盛 宴</p>
              <el-icon class="football-icon" ><Football /></el-icon>
              <div class="overlay-background">
                <p class="overlay-text">享受足球魅力<br>->赛事</p>
              </div>
          </div>

          <div class="module" @click="redirectToForum" @mouseover="hideContent" @mouseout="showContent">
              <p class="module-text">一 个 充 满 热 情<br>的 思 想 空 间</p>
              <el-icon class="posts-icon" ><ChatDotSquare /></el-icon>
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
import axios from 'axios';
import { ElIcon, ElMessage } from 'element-plus';
import carousel1 from '../assets/img/home_slider1.jpg';
import carousel2 from '../assets/img/home_slider2.jpg';
import carousel3 from '../assets/img/home_slider3.jpg';


export default {
  data() {
    return {
      hoveredIndex: -1, //帖子内容变色
      line1: '回到',
      line2: '顶部',
      newsList: [
        {
          title: "wyh好帅",
          summary: "wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅wyh好帅",
          image: "../src/assets/img/carousel3.png",
          link: "https://sse.tongji.edu.cn/"
        },
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
        },
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
        },
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
        },
        {
          title: "王晗天天拉屎",
          summary: "有消息人士称301寝室的wh一天能拉三泡屎，请跟随小编一起来看看吧",
          image: "../src/assets/img/carousel1.png",
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
      maxNewsItems: 13,
      maxPostsItems: 10,
      maxNewsLength: 30,
      GameSelect: "ALL",
      activeName: 'second',
      pageNumber: 1,
      pageSize: 10,  
      totalPosts: 0,
      showPage: false, //初始为false 向后端请求完数据后变为true 更换tag页面暂时变为false
      post_id: [],  //存储返回的帖子id
      post_title: [],  //存储返回的帖子标题
      post_content:[],
      post_likes:[],
      post_stars:[],
      currentTag: 'ALL',  //向后端传递当前页面的帖子类型 初始为全部 不受tag影响
    }
  },
  components: {
    'my-nav': MyNav
  },
  mounted() {
    this.getPosts(1, this.pageSize, this.currentTag);
  },
  methods: {
    async getPosts(pageNumber, pageSize, currentTag) {
            let response
            try {
                response = await axios.post('/api/Forum/GetPostbyLike', {
                    page: pageNumber,
                    count: pageSize,
                    tag: String(currentTag),
                }, {})
            } catch (err) {
                ElMessage({
                    message: '获取帖子失败',
                    grouping: false,
                    type: 'error',
                });
            }

            console.log('response:', response.data);
            this.post_id = [];
            this.post_title = [];
            this.post_content=[];
            this.post_likes=[];
            this.post_stars=[];
            if ( response.data.postInfoJsons) {
                response.data.postInfoJsons.forEach((postInfo) => {
                    this.post_id.push(postInfo.post_id);
                    this.post_title.push(postInfo.title);
                    this.post_content.push(postInfo.contains);
                    this.post_likes.push(postInfo.approvalNum);
                    this.post_stars.push(postInfo.collectNum);
                });
            }
            else {
                ElMessage({
                    message: '后端返回的帖子数据格式错误',
                    grouping: false,
                    type: 'error',
                });
            }
            
            //console.log('得到的帖子id为:', this.post_id);
            //console.log('得到的帖子title为:', this.post_title);
        },
    getColorClass(index) {    //设置热帖序号颜色
      if (index < 3) {
        const colors = ['color-red', 'color-green', 'color-blue']; // Add your desired colors here
        return colors[index];
      } else {
        return 'color-gray'; // Default gray color for other numbers
      }
    },
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
    // getLimitedPosts() {
    //   return this.postsList.slice(0, this.maxPostsItems);
    // },
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

<style scoped>
.main-container{
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column; /* 垂直方向排列子组件 */
}

.top-section {
  width: 86vw;
}
/* 赛事种类选择栏 */
.Games-menu {
  padding: 10px;
  background-color: #78b9fa;
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
  color: #409EFF;
}

/* 赛事信息卡片 */
.Game-col-container {
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
  padding-top: 10px;
  justify-content: space-between;
  width: 86vw;
  align-items: flex-start; /* Align items at the top */
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
  overflow: hidden; /* Hide overflow content */
}

.posts-column {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: flex-start;
}

.posts-list {
  display: flex;
  flex-direction: column;
  gap: 20px; 
}

.post-number {
  font-weight: bold;
  margin-right: 5px;
}

.color-red {
  color: #e74c3c; /* Your desired color for the first number */
}

.color-green {
  color: #27ae60; /* Your desired color for the second number */
}

.color-blue {
  color: #3498db; /* Your desired color for the third number */
}

.color-gray {
  color: #8e8e8e; /* Default gray color for other numbers */
}


.posts-item {
  margin-bottom: 10px;
  width: 100%;
  border: 1px solid #ccc;
  background-color: #fff;
  transition: transform 0.3s, box-shadow 0.3s;
  cursor: pointer;
  border-radius:10px;
}

.posts-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.posts-link {
  display: flex;
  flex-direction: column;
  height: 100%;
  text-decoration: none;
  color: #333;
}

.posts-item-wrapper {
  display: flex;
  align-items: stretch;
  padding: 10px;
}

.posts-content {
  flex-grow: 1;
}

.posts-title {
  font-size: 18px;
  margin: 0;
}

.posts-summary {
  margin: 10px 0;
  line-height: 1.4;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
}

.posts-summary.hovered-summary {
  color: #3498db; /* Light blue color when hovered */
}

.info-group {
  display: flex;
  align-items: center; 
  margin-top: 10px; 
}

.space-between {
  width: 20px; 
}

.like-container,
.star-container {
  display: flex; /* Display icon and number in the same line */
  align-items: center;
  gap: 5px; /* Adjust the gap between icon and number */
}

.like-number,
.star-number {
  font-size: 14px; /* Adjust the font size as needed */
}

.hot-posts {
  font-size: 36px;
  font-weight: bold;
  color: #4fb3ffc7;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* 新闻模块 */

.news-container {
  width: 60%;
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin-top: 20px;
  justify-content: center;
}

.hot-news {
  font-size: 36px;
  font-weight: bold;
  color: #4fb3ffc7;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
}


.news-row {
  margin-top: 10px;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  gap: 10px;
}

.news-item {
  width: calc(33% - 10px);
  border: 1px solid #ccc;
  overflow: hidden;
  background-color: #fff;
  transition: transform 0.3s, box-shadow 0.3s;
  cursor: pointer;
  margin-bottom: 10px;
  border-radius:10px;
  box-sizing: border-box;
}

.news-item.two-columns {
  flex-basis: calc(66.66% - 10px);
}

.news-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.news-link {
  display: flex;
  flex-direction: column;
  height: 100%;
  text-decoration: none;
  color: #333;
}

.news-item-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100%;
}

.news-content {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px;
  height: 100%;
}

.news-image-wrapper {
  width: 100%;
  padding: 10px;
}

.news-image {
  max-width: 100%;
  height: auto;
  margin-bottom: 10px;
}

.news-title {
  font-size: 18px;
  margin: 0;
}

.news-summary {
  
  margin: 10px 0;
  line-height: 1.4;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3; /* Number of lines to show */
  -webkit-box-orient: vertical;
}

/* 右下组件——回到顶部 */
.scroll-to-top-btn {
  position: fixed;
  bottom: 20px;
  right: 20px;
  width: 50px;
  height: 50px; 
  cursor: pointer;
  overflow: hidden; 
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
  font-size: 16px;
}

/* <!-- 下半部分 --> */
/* 赛事，论坛，新闻选择 */
.bottom-section {
  width: 86vw;
  display: flex;
  justify-content: space-around;
  padding: 20px;
}

/* 设置模块样式 */
.module {
  position: relative;
  text-align: center;
  padding: 20px;
  background-color: rgb(241, 204, 253);
  border: 1px solid #ccc;
  border-radius: 10px;
  width: 30%;
  height: 120px;
  cursor: pointer; /* 添加交互：将鼠标光标变为手型 */
  transition: transform 0.3s ease;
}

.module:hover {
    transform: scale(1.05);
}
.module .module-text {
  font-size: 18px;
  color: rgba(0, 123, 255, 0.7);
  margin-top:1%;
}

.news-icon,
.posts-icon,
.football-icon {
  font-size: 40px; /* Adjust the size of the icon as needed */
  margin-bottom: 10px; /* Add margin between icon and text */
  color: rgba(0, 123, 255, 0.7);
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

.module .module-text, .module .football-icon,
.module .news-icon,.module .posts-icon{
  visibility: visible;
}

.module:hover .module-text, .module:hover .football-icon,
.module:hover .news-icon,.module:hover .posts-icon{
  visibility: hidden;
}
</style>


<!-- <button @click="this.$router.push('/signin')">登录</button>
<button @click="this.$router.push('/signup')">注册</button> -->
