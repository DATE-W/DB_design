<!-- 2154314_郑楷_赛事列表 2023.08.11 14:00 v1.6.0
 v1.0.0 页面画了一半 
 v1.1.0 画出了左侧的联赛选择器（未添加逻辑），布局了中部的比赛列表（已添加跳转逻辑），增加了各大联赛LOGO素材图，日期选择器和广告区待实现
 v1.2.0 优化了联赛选择器组件的代码、视觉效果、功能、数据通路
 v1.3.0 中间一列赛事缩略图的代码简化与传值
 v1.4.0 功能基本完成，前后端接口已经对齐，能完成实时渲染
 v1.5.0 增加跳转到赛事详情页的通路，并完成传值，添加注释
 v1.6.0 与赛事详情页互相传值，使得在返回本页面时，之前的日期与联赛选择不会丢失 -->

<template>
  <my-nav></my-nav>
  <!-- 左侧联赛选择器 -->
  <div class="borderBoxLeft" style="left:5rem;">
    <!-- 使用v-for指令循环生成联赛选择器内容 -->
    <div class="borderBoxLeague" v-for="(league, index) in leagues" :key="index"
      :style="{ top: `${index * 5 + 0.4}rem`, background: ((index == league11) ? 'aqua' : '') }"
      @click="leagueChoice(index)">
      <!-- 插入联赛LOGO图片 -->
      <img v-if="league.logo" :src="league.logo" class="imgLogo">
      <!-- 将top值调整为合适的位置，同时调整“全部赛事”和“其他赛事”的位置 -->
      <p class="textTypoLeague" :style="{ top: '-1.5rem', left: ((0 == index || 7 == index) ? '2.5rem' : '6rem') }">{{
        league.name }}
      </p>
    </div>
  </div>

  <!-- 中间列时间与赛事列表 -->
  <div class="borderBoxMid" style="left:27rem">

    <!-- test -->
    <p class="textTypoLeague" style="left:0rem;width:30rem;">当前选择日期: {{ date11 }}</p>

    <!-- 使用v-for循环生成赛事列表 -->
    <div class="borderBoxMatch" v-for="(match, index) in matches" :key="match.gameUid"
      :style="{ top: `${index * 6 + 5}rem` }">
      <!-- 根据matches数据渲染赛事列表的内容 -->
      <div class="imgBox">
        <img :src="match.homeLogo">
        <div class="modal2"></div>
      </div>
      <div class="imgBox" style="left:29.4rem">
        <img :src="match.guestLogo">
        <div class="modal2"></div>
      </div>
      <div class="borderBoxMatchModal" @click="toMatchDetail(match.gameUid, this.league11, this.date11)">
        <p class="textTypoMatchTime">{{ match.startTime }}</p>
        <p class="textTypoMatchTeam">{{ match.homeTeamName }}</p>
        <p class="textTypoMatchScore">{{ scoreCheck(match.homeScore) }} - {{ scoreCheck(match.guestScore) }}</p>
        <p class="textTypoMatchTeam" style="left:auto;right:1rem">{{ match.guestTeamName }}</p>
        <p class="textTypoMatchStatus">{{ getMatchStatus(match.status) }}</p>
      </div>
    </div>

  </div>
  <!-- 右侧上方日期选择器容器 -->
  <div class="borderBoxRightTop" style="left:74rem">
    <!-- 日期选择器 -->
    <el-date-picker v-model="date11" type="date" placeholder="日期选择" :size="large" value-format="YYYY-MM-DD"
      style="left:1.5rem;top:5rem" @change="this.getMatches(this.date11, this.league11);" />
  </div>
  <!-- 右侧下方主队容器 -->
  <div class="borderBoxRightAD" style="left:74rem;">
    <!-- <button @click="console.log(recentMatches);">1</button>
    <button @click="getRecentMatches('利物浦');">2</button> -->
    <p>主队: {{ mainTeam }}</p>
    <div class="borderBoxRecentMatch" v-for="(recentMatch, index) in recentMatches" :key="recentMatch.gameUid"
      :style="{ top: `${index * 5.5 + 3}rem` }">
      <!-- <p>{{ recentMatch.gameUid }}</p> -->
      <div class="imgBox">
        <img :src="recentMatch.opponentLogo">
        <div class="modal2"></div>
      </div>
      <div class="borderBoxRecentMatchModal" @click="toMatchDetail(recentMatch.gameUid, this.league11, this.date11)">
        <p class="textRecentMatchScore">{{ recentMatch.homeScore }}</p>
        <p class="textRecentMatchScore" style="left:3rem;color:black"> - {{ recentMatch.opponentScore }}</p>
        <p class="gameDateR">{{ recentMatch.gameDate }}</p>
        <p class="textVsComponent">VS {{ recentMatch.opponentName }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import MyNav from './nav.vue';
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import { ref } from 'vue';

export default {

  components: {
    'my-nav': MyNav
  },

  created() {
    if (this.$route.query.date11) {
      this.date11 = this.$route.query.date11;
    }
    this.league11 = this.$route.query.league11;
  },

  mounted() {
    // 在页面挂载后获取赛事列表数据
    this.getMatches(this.date11, this.league11);
    this.getRecentMatches(this.mainTeam);
    /* this.getMainTeam(); */
  },

  methods: {
    // 跳转到赛事详情页
    toMatchDetail(uid, leagueC, dateC) {
      this.match11 = uid;
      //etTimeout(function(){ getSignature() },5000);//Test
      this.$router.push(
        {
          path: `/detailedMatch`,
          query: {
            gameUid: uid,
            lgeChoice: leagueC,
            dateChoice: dateC,
          }
        }
      );
    },
    // 选择联赛
    leagueChoice(choice) {
      if (this.league11 != choice) {
        this.league11 = choice;
      }
      else {
        this.league11 = 0;
      }
      this.getMatches(this.date11, this.league11);
    },
    // 比分空值处理
    scoreCheck(score) {
      if (!score) {
        return 0;
      }
      else {
        return score;
      }
    },
    // 根据赛事状态返回对应的文字描述
    getMatchStatus(status) {
      switch (status) {
        case 'Played':
          return '已结束';
        case 'Unplayed':
          return '未开始';
        case 'Postponed':
          return '比赛延期';
        case 'Playing':
          return '进行中';
        default:
          return '未知';
      }
    },
    // 将日期对象转换为字符串
    dateToString(date) {
      var year = date.getFullYear();
      var month = (date.getMonth() + 1).toString();
      var day = (date.getDate()).toString();
      if (month.length == 1) {
        month = "0" + month;
      }
      if (day.length == 1) {
        day = "0" + day;
      }
      var dateTime = year + "-" + month + "-" + day;
      return dateTime;
    },
    // 调用接口获取赛事列表数据
    async getMatches(dateCho, leagueCho) {
      let response
      try {
        response = await axios.post('/api/updateTeam/searchTeamInGameTime', {
          dateTime: dateCho,
          gameType: leagueCho,
        }, {})
      } catch (err) {
        ElMessage({
          message: '获取赛事数据失败',
          grouping: false,
          type: 'error',
        });
      }
      /* console.log(dateCho,leagueCho); */
      this.matches = response.data;
      console.log(this.matches);
    },
    // 调用接口获取主队近期三场赛事
    async getRecentMatches(mainTeam) {
      let response
      try {
        response = await axios.post('/api/updateTeam/getTeamMatchesByName', {
          teamName: mainTeam,
        }, {})
      } catch (err) {
        ElMessage({
          message: '获取主队近期赛事数据失败',
          grouping: false,
          type: 'error',
        });
      }
      /* console.log(dateCho,leagueCho); */
      this.recentMatches = response.data;
      console.log(this.recentMatches);
    },
    // 调用接口获取主队名
    async getMainTeam() {
      let response
      try {
        response = await axios.post('/api/User/profile', {
          teamName: mainTeam,
        }, {})
      } catch (err) {
        ElMessage({
          message: '获取主队信息失败',
          grouping: false,
          type: 'error',
        });
      }
      /* console.log(dateCho,leagueCho); */
      this.mainTeam = response.data.utf;
      /* console.log(this.matches); */
    },

  },

  setup() {
    const getAssetsImages = (name) => {
      return new URL(league.logo, import.meta.url).href; //本地文件路径
    }
  },

  data() {
    return {

      league11: 0,
      match11: 0,
      date11: ref(this.dateToString(new Date())),

      leagues: [
        { name: "全部赛事", logo: "" },
        { name: "英超", logo: "/src/assets/img/pmlogo.png" },
        { name: "西甲", logo: "/src/assets/img/lllogo.png" },
        { name: "意甲", logo: "/src/assets/img/salogo.png" },
        { name: "德甲", logo: "/src/assets/img/bllogo.png" },
        { name: "法甲", logo: "/src/assets/img/le1logo.png" },
        { name: "中超", logo: "/src/assets/img/cslogo.png" },
        { name: "其他赛事", logo: "" },],

      /* 正式版本 */
      matches: ref([]),
      recentMatches: ref([]),
      /* mainTeam: ref(''), */

      /* 测试版本 */
      mainTeam: ref('利物浦'),
      /* recentMatches: [
        {
          "gameDate": "2023-08-12",
          "homeTeam": 50000532,
          "opponentTeam": 50000540,
          "opponentName": "谢菲尔德联",
          "homeScore": 1,
          "opponentScore": 0,
          "opponentLogo": "https://sd.qunliao.info/fastdfs4/M00/D1/27/ChNLkl1L9hOAFN_9AABizeiU33g983.png",
          "gameUid": "53614996"
        },
        {
          "gameDate": "2023-03-15",
          "homeTeam": 50000532,
          "opponentTeam": 50000556,
          "opponentName": "布莱顿",
          "homeScore": 0,
          "opponentScore": 1,
          "opponentLogo": "https://sd.qunliao.info/fastdfs3/M00/B5/75/ChOxM1xC2F6ACzweAAATm2O1vDk447.png",
          "gameUid": "53396655"
        },
        {
          "gameDate": "2023-01-18",
          "homeTeam": 50000532,
          "opponentTeam": 50000515,
          "opponentName": "曼联",
          "homeScore": 1,
          "opponentScore": 1,
          "opponentLogo": "https://sd.qunliao.info/fastdfs3/M00/B5/75/ChOxM1xC2FWAK5dCAAAmr0XTTPA012.png",
          "gameUid": "53396643"
        }
      ] */

    }
  }
}


</script>


<style scoped>
/* 框体样式 */

/* 左侧容器框 */
.borderBoxLeft {
  position: absolute;
  width: 15rem;
  height: 40.5rem;
  flex-shrink: 0;
  /* 正式版本 */
  background: rgb(240, 240, 240);
  /* 测试版本 */
  /* background: #4BDFBC;  */
}

/* 中部容器框 */
.borderBoxMid {
  position: absolute;
  width: 40rem;
  height: 40rem;
  flex-shrink: 0;
  /* 正式版本 */
  background: white;
  /* 测试版本 */
  /* background: #E174C3;  */
}

/* 右侧上方容器框 */
.borderBoxRightTop {
  position: absolute;
  width: 17rem;
  height: 15rem;
  flex-shrink: 0;
  /* 正式版本 */
  background: rgb(240, 240, 240);
  /* 测试版本 */
  /* background: #4BDFBC; */
}

/* 右侧下方容器框 */
.borderBoxRightAD {
  position: absolute;
  width: 17rem;
  height: 20rem;
  flex-shrink: 0;
  top: 20rem;
  /* 正式版本 */

  /* 测试版本 */
  /* background: #4B8FDF; */
}

.imgBox {
  position: absolute;
  left: 5rem;
  width: 10rem;
  height: 4rem;
}

.modal1 {
  position: absolute;
  left: 0rem;
  top: 0rem;
  width: 10rem;
  height: 4rem;
  background-color: rgba(255, 255, 255, 0.5);
  transition: background-color 0.8s ease;
}

.modal2 {
  position: absolute;
  left: 0rem;
  top: 4rem;
  width: 10rem;
  height: 5.5rem;
  background-color: white;
}

/* 联赛选择按钮 */
.borderBoxLeague {
  position: absolute;
  width: 13rem;
  height: 4rem;
  flex-shrink: 0;
  border-radius: 2rem;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  left: 1rem;
  background-color: #ffffff;
  transition: background-color 0.8s ease;
}

.borderBoxLeague:hover {
  background-color: rgb(246, 77, 77);
}

/* 联赛名称框 */
.borderBoxText1 {
  position: absolute;
  width: 8rem;
  height: 2rem;
  top: 1rem;
  left: 4rem;

  /* 测试版本，正式版本删去 */
  background: white;
}

/* 各场赛事框 */
.borderBoxMatch {
  position: absolute;
  width: 39.8rem;
  height: 4rem;
  flex-shrink: 0;
  border-radius: 1.4rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1);
  transition: background-color 0.8s ease;
  /* 正式版本 */
  background: white;
}

.borderBoxMatchModal {
  position: absolute;
  width: 39.8rem;
  height: 4rem;
  flex-shrink: 0;
  border-radius: 1.4rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1);
  transition: background-color 0.8s ease;
  /* 正式版本 */
  background: rgba(255, 255, 255, 0.7);
}

.borderBoxMatchModal:hover {
  background: rgba(240, 240, 240, 0.9);
}

.borderBoxRecentMatch {
  position: absolute;
  width: 17rem;
  height: 4rem;
  flex-shrink: 0;
  border-radius: 1.5rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1);
  transition: background-color 0.8s ease;
  /* 正式版本 */
  background: white;
}

.borderBoxRecentMatchModal {
  position: absolute;
  width: 17rem;
  height: 4rem;
  flex-shrink: 0;
  border-radius: 1.5rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1);
  transition: background-color 0.8s ease;
  /* 正式版本 */
  background: rgba(255, 255, 255, 0.7);
}

.borderBoxMatch:hover {
  background-color: rgb(240, 240, 240);
}

.borderBoxRecentMatch:hover {
  background-color: rgb(240, 240, 240);
}

.borderBoxRecentMatchModal:hover {
  background-color: rgba(240, 240, 240, 0.9);
}



/* 字体样式 */

/* 左侧联赛名称字体样式 */
.textTypoLeague {
  position: absolute;
  width: 8rem;
  height: 2rem;
  top: -1.3rem;
  left: 6rem;
  color: var(--colors-text-dark-172239100, #172239);
  font-feature-settings: 'clig' off, 'liga' off;
  font-family: Verdana;
  font-size: 2rem;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.textTypoMatchTime {
  position: absolute;
  color: rgb(17, 60, 158);
  font-family: Georgia;
  font-size: 1.5rem;
  font-style: normal;
  font-weight: 100;
  line-height: normal;
  top: -1rem;
  left: 1rem;
}

.gameDateR {
  position: absolute;
  color: rgb(17, 60, 158);
  font-family: Georgia;
  font-size: 0.9rem;
  font-style: normal;
  font-weight: 100;
  line-height: normal;
  top: -1rem;
  right: 1rem;
}

.textTypoMatchTeam {
  position: absolute;
  color: rgb(0, 0, 0);
  font-family: Impact;
  font-size: 2rem;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  top: -1.5rem;
  left: 6rem;
}

.textTypoMatchStatus {
  position: absolute;
  color: rgb(97, 97, 97);
  font-family: Impact;
  font-size: 0.8rem;
  font-style: normal;
  font-weight: 200;
  line-height: normal;
  top: 1.6rem;
  left: 1.7rem;
}

.textTypoMatchScore {
  position: absolute;
  color: rgb(255, 0, 0);
  font-family: Verdana;
  font-size: 2.5rem;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
  top: -2.2rem;
  left: 18rem;
}

.textRecentMatchScore {
  position: absolute;
  color: rgb(255, 0, 0);
  font-family: Verdana;
  font-size: 2rem;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
  top: -1.2rem;
  left: 1rem;
}

.textVsComponent {
  position: absolute;
  color: black;
  font-family: Verdana;
  font-size: 1rem;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  top: 0.4rem;
  left: 7rem;
}

/* 图片样式 */

/* 联赛图标 */
.imgLogo {
  position: absolute;
  width: 4rem;
  height: 4rem;
  border-radius: 2rem;
  left: 1rem;
  object-fit: cover;
}
</style>