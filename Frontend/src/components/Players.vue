<!-- 
2152190_李凌朗_球员信息 v1.0.0
 v1.0.0 设计页面布局，与赛事界面统一左侧联赛选择方式，具体广告区、球队球员区跳转逻辑待实现，界面布局待优化
 v1.0.1 重构页面布局，添加图片对布局进行完善
-->
<template>
  <my-nav></my-nav>

  <!-- 左侧联赛选择器 -->
  <el-card class="left" style="left:5rem">
    <!-- 使用v-for指令循环生成联赛选择器内容 -->
    <div class="leagueStyle" v-for="(league, index) in leagues" :key="index"
      :style="{ top: `${index * 5 + 0.8}rem`, background: ((index === leagueNo) ? 'aqua' : '') }"
      @click="leagueChoice(index)">
      <!-- 插入联赛LOGO图片 -->
      <img v-if="league.logo" :src="league.logo" class="imgLogo">
      <!-- 将top值调整为合适的位置，同时调整“全部赛事”和“其他赛事”的位置 -->
      <p class="textTypeLeague" :style="{ top: '-1.5rem', left: ((0 == index || 7 == index) ? '2.5rem' : '6rem') }">{{
        league.name }}
      </p>

    </div>
  </el-card>

  <!-- 视频 -->
  <div class="videoContainer">
    <el-carousel :interval="5000">
      <el-carousel-item v-for="(video, index) in videoList" :key="index">
        <a :href="video.url" target="_blank">
          <img class="videoCover" :src="video.cover" @click="goToVideo(video.url)" />
        </a>
      </el-carousel-item>
    </el-carousel>

  </div>

  <!-- 搜索栏 -->
  <div class="search-container">
    <el-icon class="search-icon">
      <Search />
    </el-icon>
    <el-input class="search-input" v-model="keyword" placeholder="请输入关键词" @keyup.enter.native="handleSearch(keyword)">
    </el-input>
  </div>

  <!-- 获取球队信息 -->
  <div class="team" v-for="(team, index) in teamList" :key="index"
    :style="{ top: `${Math.floor(index / 4) * 6 + 18}rem`, left: `${index % 4 * 12 + 22}rem` }"
    @click="direct2TeamMsg(team.teamName)">
    <img class="teamLogo" :src="team.teamLogo">
    <div class="teamNameContainer">
      <p class="teamName">
        {{ team.teamName }}
      </p>
    </div>
  </div>

  <!-- 射手榜 -->
  <el-card class="topScorerContainer" style="right:5vw;">
    <div class="topScorerHeader">
      <!-- -->
      <p class="topScorerFont">射手榜</p>
    </div>
    <!---->
    <div class="singleTopScorer" v-for="(topScorer, index) in topScorerList" :key="index"
      :style="{ top: `${index * 3}rem` }" @click="direct2detailedPlayerMsg(topScorer.topScorerName)">
      <p>{{ topScorer.topScorerName }}--------------{{ topScorer.goals }}</p>
    </div>

  </el-card>
</template>

<script>
import axios from 'axios';
import MyNav from './nav.vue';
import { ElMessage } from 'element-plus';
import { ref } from 'vue';
import { ElCarousel, ElCarouselItem } from 'element-plus';

export default {
  components: {
    'my-nav': MyNav,
    ElCarousel,
    ElCarouselItem,
  },

  created() {
    this.getBasicTeamMsg(0);
    this.getTopScorerMsg();
    this.getSearchedPlayer('梅');

  },

  methods: {
    direct2TeamMsg(teamName) {
      this.$router.push({
        path: '/teamMsg',
        query: {
          teamName: teamName
        }
      })
    },

    direct2detailedPlayerMsg(topScorerName) {
      this.$router.push({
        path: '/detailedPlayerMsg',
        query: {
          playerName: topScorerName
        }
      });
    },

    leagueChoice(choice) {
      if (this.leagueNo != choice) {
        this.leagueNo = choice;
      }
      else {
        this.leagueNo = 0;
      }

      this.getBasicTeamMsg(choice);
    },

    goToVideo(videoUrl) {
      window.open(videoUrl, '_blank');
    },

    handleSearch(keyWord) {
      this.getSearchedTeam(keyWord);
      this.getSearchedPlayer(keyWord);
    },

    async getBasicTeamMsg(leagueNo) {
      let response;
      try {
        response = await axios.post('/api/updateTeam/searchTeamInGameType', {
          gameType: leagueNo,
        });

        this.teamList = [];
        this.teamList = response.data;
        console.log(response);

      } catch (err) {
        ElMessage({
          message: '获取信息失败',
          type: 'error',
        });
      }

    },

    async getTopScorerMsg() {
      let response;
      try {
        response = await axios.get('/api/updateTeam/getTopScorers');
        this.topScorerList = [];
        this.topScorerList = response.data;

      } catch (err) {
        ElMessage({
          message: '获取射手榜失败',
          type: 'error',
        });
      }
    },

    async getSearchedPlayer(keyWord) {
      let response;
      try {
        response = await axios.post('/api/updateTeam/searchForPlayer', {
          key: keyWord,
        });

        this.searchedPlayer = [];
        this.searchedPlayer = response.data;

      } catch (err) {
        ElMessage({
          message: '搜索失败',
          type: 'error',
        });
      }
    },

    async getSearchedTeam(keyWord) {
      let response;
      try {
        response = await axios.post('/api/updateTeam/searchForTeam', {
          key: keyWord,
        });

        this.teamList = [];
        for (let i = 0; i < response.data.length; i++) {
          const newTeam = { teamLogo: response.data[i].searchedTeamLogo ,teamName:response.data[i].searchedTeamName}; // 创建新对象并设置属性
          this.teamList.push(newTeam); // 将对象添加到数组中
        }

      } catch (err) {
        ElMessage({
          message: '搜索失败',
          type: 'error',
        });
      }

    },
  },


  data() {
    return {
      leagueNo: 0,
      match11: 0,
      keyword: '',

      leagues: [
        { "name": "全部赛事", "logo": "" },
        { "name": "英超", "logo": "/src/assets/img/pmlogo.png" },
        { "name": "西甲", "logo": "/src/assets/img/lllogo.png" },
        { "name": "意甲", "logo": "/src/assets/img/salogo.png" },
        { "name": "德甲", "logo": "/src/assets/img/bllogo.png" },
        { "name": "法甲", "logo": "/src/assets/img/le1logo.png" },
        { "name": "中超", "logo": "/src/assets/img/cslogo.png" },
        { "name": "其他赛事", "logo": "" },
      ],

      teamList: ref([
        { "teamName": "c102", "teamLogo": "/src/assets/img/pmlogo.png" },
        { "teamName": "原神", "teamLogo": "/src/assets/img/pmlogo.png" },
        { "teamName": "明日方舟", "teamLogo": "/src/assets/img/pmlogo.png" },
        { "teamName": "cp30", "teamLogo": "/src/assets/img/pmlogo.png" },
        { "teamName": "我好喜欢啊啊啊", "teamLogo": "/src/assets/img/lllogo.png" },
      ]),

      topScorerList: ref([
        { "topScorerName": " wjl ", "goals": 1919810 },
        { "topScorerName": " wyh ", "goals": 114514 },
        { "topScorerName": " wh ", "goals": 1 },
        { "topScorerName": " wrb ", "goals": 11514 },
        { "topScorerName": " lll ", "goals": 114 },
        { "topScorerName": " zk ", "goals": 4 },
        { "topScorerName": " xjq ", "goals": 114514 },
        { "topScorerName": " lth ", "goals": 114514 },
        { "topScorerName": " wlq ", "goals": 114514 },
        { "topScorerName": " zyp ", "goals": 114514 },
      ]),

      searchedPlayer: ref([

      ]),

      videoList: [
        {
          url: 'https://www.bilibili.com/video/BV1NP41127r7/?spm_id_from=333.337.search-card.all.click&vd_source=698fdafb635dd8f2216051208c318bcb',
          cover: '/src/assets/img/carousel1.png',
        },
        {
          url: 'https://www.bilibili.com/video/BV1NP41127r7/?spm_id_from=333.337.search-card.all.click&vd_source=698fdafb635dd8f2216051208c318bcb',
          cover: '/src/assets/img/carousel2.png',
        },
      ],
      currentIndex: 0,
    }
  }
}
</script>

<style scoped>
/* 左侧容器 */
.left {
  position: absolute;
  width: 15vw;
  height: 90vh;
  flex-shrink: 0;
  background: rgb(240, 240, 240);
}

/* 联赛选择按钮 */
.leagueStyle {
  position: absolute;
  width: 13vw;
  height: 4vw;
  flex-shrink: 0;
  border-radius: 2vw;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  left: 1vw;
  background-color: #ffffff;
  transition: background-color 0.8s ease;
}

.leagueStyle:hover {
  background-color: rgb(246, 77, 77);
}

/* 搜索框 */
.search-container {
  margin-top: 21vh;
  margin-left: 23.5vw;
  display: flex;
  align-items: center;
}

.search-input {
  width: 30%;
}

.search-icon {
  font-size: 25px;
}

.page-container {
  position: absolute;
  bottom: 0px;
  left: 55%;
  transform: translateX(-50%);
}


/* 左侧联赛名称字体样式 */
.textTypeLeague {
  position: absolute;
  width: 8vw;
  height: 2vw;
  top: -1.3vw;
  left: 6vw;
  color: var(--colors-text-dark-172239100, #172239);
  font-feature-settings: 'clig' off, 'liga' off;
  font-family: Verdana;
  font-size: 2vw;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* 联赛图标 */
.imgLogo {
  position: absolute;
  width: 4vw;
  height: 4vw;
  border-radius: 2vw;
  left: 1vw;
  object-fit: cover;
}

.videoContainer {
  position: absolute;
  width: 50vw;
  height: 7.5vw;
  top: 2vh;
  left: 25vw;
}

.videoCover {
  position: absolute;
  width: 50vw;
  height: 7.5vw;
  top: 10vh;
  left: 0vw;
}

/* 球队按钮大小 */
.team {
  position: absolute;
  width: 11vw;
  height: 6vh;
  flex-shrink: 0;
  border-radius: 2vw;
  display: flex;
  /* 添加flex布局 */
  align-items: center;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  background: linear-gradient(to right, #d2fcfb, #d7f1f0);
}

.teamNameContainer {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* 球队名称 */
.teamName {
  display: flex;
  align-items: center;
  text-align: center;
  justify-content: center;
  white-space: nowrap;

}

/* 球队logo */
.teamLogo {
  margin-left: 10px;
  height: 4vh;
  width: 2vw;
}

/* 射手榜 */
.topScorerContainer {
  position: absolute;
  width: 17vw;
  height: 85vh;
  flex-shrink: 0;
  bottom: 2vw;
  display: flex;
  /* 添加 Flex 布局 */
  flex-direction: column;
  /* 垂直排列子元素 */
  justify-content: flex-start;
  /* 将子元素放在容器顶部 */
  background: rgb(240, 240, 240);
}

/* 射手榜抬头 */
.topScorerHeader {
  width: 14vw;
  height: 6vh;
  top: 400%;
  /*
*/
  background: white;

}

/* 抬头字体 */
.topScorerFont {
  /** */
  align-items: center;
  text-align: center;
  font-feature-settings: 'clig' off, 'liga' off;
  font-family: Verdana;
  font-size: 2vw;
  font-style: normal;
  font-weight: 600;
}

/* 射手榜单个射手 */
.singleTopScorer {
  width: 14vw;
  height: 5vh;
  margin-left: 25px;
  /* 
*/
  background: white;



}
</style>

 
