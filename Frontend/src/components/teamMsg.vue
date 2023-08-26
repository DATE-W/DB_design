<template>
  <my-nav></my-nav>
  <el-card class="topContainer">
    <img class="teamIcon" alt="This is team icon" :src="this.logo">
    <div class="teamInfo">

      <div class="name">
        <p class="headerText" style="left: 50%; transform: translate(-50%, -50%); font-size: 2vw;">
          {{ this.teamName }}
        </p>

      </div>
      <div class="enName">
        <p class="headerText" style="top: -40%; left: 50%; transform: translate(-50%, -50%); font-size: 1.2vw;">
          {{ this.enName }}
        </p>
      </div>

      <div class="firstBar">
        <div class="firstBlock">
          成立：{{ this.foundYear }}
        </div>
        <div class="secondBlock">
          国家：{{ this.country }}
        </div>
        <div class="thirdBlock">
          城市：{{ this.city }}
        </div>
      </div>
      
      <div class="secondBar">
        <div class="firstBlock">
          主场：{{ this.venueName }}
        </div>
        <div class="secondBlock">
          容纳：{{ this.venueCapacity }}人
        </div>
      </div>

      <div class="thirdBar">
        <div class="firstBlock">
          电话：{{ this.tel }}
        </div>
        <div class="secondBlock">
          邮箱：{{ this.email }}
        </div>
      </div>

      <div class="fourthBar">
        <div class="firstBlock">
          地址：{{ this.address }}
        </div>
      </div>
<!-- -->
    </div>
  </el-card>

  <el-card class="playerInfo" v-for="(singleteamMember, index) in teamMember" :key="index"
    :style="{ top: `${Math.floor(index / 4) * 19 + 26}rem`, left: `${index % 4 * 16 + 16}rem` }"
    @click="direct2detailedPlayerMsg(singleteamMember.playerName)">
    <img class="playerPic" :src="singleteamMember.playerPhoto">
    <p class="playerName" style="left: 50%; transform: translate(-50%, -50%); font-size: 1.2vw;">
      {{ singleteamMember.playerName }}
    </p>
  </el-card>
</template>



<script>
import { ref } from 'vue';
import MyNav from './nav.vue';
import { ElMessage } from 'element-plus';
import axios from 'axios';

export default {
  components: {
    'my-nav': MyNav
  },

  created() {
    this.teamName = this.$route.query.teamName;
  },

  mounted() {
    this.getTeamMsg(this.teamName);

  },

  methods: {
    direct2detailedPlayerMsg(topScorerName) {
      this.$router.push({
        path: '/detailedPlayerMsg',
        query: {
          playerName: topScorerName
        }
      });
    },




    async getTeamMsg(teamName) {
      let response;
      try {
        response = await axios.post('/api/updateTeam/getTeamInfoByName', {
          teamName: teamName,
        });

//        console.log(response);
        this.recentGames = [];
        this.teamMember = [];

        this.city = response.data[0].city;
        this.foundYear = response.data[0].foundYear;
        this.enName = response.data[0].enName;
        this.logo = response.data[0].logo;
        this.country = response.data[0].country;
        this.venueName = response.data[0].venue_name;
        this.tel = response.data[0].telephone;
        this.address = response.data[0].address;
        this.venueCapacity = response.data[0].venue_capacity;
        this.email = response.data[0].email;

        this.recentGames = response.data[0].recentGames;
        this.teamMember = response.data[0].teamMember;

      } catch (err) {
        ElMessage({
          message: '获取球队信息失败',
          type: 'error',
        });
      }
    },


  },

  data() {
    return {
      teamName: '不要挂ysj挑战',
      enName: 'isafnjnjds',
      logo: '/src/assets/img/pmlogo.png',
      coach: 'wjl',
      foundYear: '2023',
      city: '龙门',
      address: '须弥城114弄514号',
      country: '璃月',
      venueName: '301',
      venueCapacity: 114514,
      tel: '+86 12345678901',
      email: '111',

      teamMember: ref([
        { "playerName": "wjl", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyhwyhwyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },

      ]),
      recentGames: ref([
        { "gameDate": "1", "opponentName": "2", "homeScore": 0, "opponentScore": 1 },
        { "gameDate": "1", "opponentName": "2", "homeScore": 0, "opponentScore": 1 },
        { "gameDate": "1", "opponentName": "2", "homeScore": 0, "opponentScore": 1 },

      ]),
      /* /*        
      
      */



    }

  }
}
</script>

<style scoped>
/* 顶部容器 */
.topContainer {
  position: absolute;
  left: 20%;
  width: 60vw;
  height: 42vh;
  flex-shrink: 0;
  background: rgb(245, 245, 245);
}

/* 队标 */
.teamIcon {
  position: absolute;
  left: 6%;
  top: 25%;
  width: 15%;
  height: 40%;
  flex-shrink: 0;
}

/* 队伍信息 */
.teamInfo {
  position: absolute;
  left: 25%;
  top: 10%;
  width: 70%;
  height: 80%;
  flex-shrink: 0;
  /*

  background: rgb(194, 249, 249);*/

}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 3vw;
  top: 12vh;
  height: 4vh;
  width: 36vw;
  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 3vw;
  top: 18vh;
  height: 4vh;
  width: 36vw;
  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 3vw;
  top: 24vh;
  height: 4vh;
  width: 36vw;
  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第四行 */
.fourthBar {
  position: absolute;
  left: 3vw;
  top: 30vh;
  height: 4vh;
  width: 36vw;
  /*

  background: rgb(0, 240, 249); */

}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;
  /*

  background: rgb(255, 255, 255); */
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 16vw;
  /*

  background: rgb(255, 255, 255); */
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 26vw;
  /*

  background: rgb(255, 255, 255); */
}

/* 球员信息 */
.playerInfo {
  position: absolute;
  left: 20%;
  top: 60%;
  width: 200px;
  height: 250px;
  flex-shrink: 0;
  
/*
  background: #0fffff; */
}

.playerPic {
  position: absolute;
  left: 5%;
  width: 180px;
  height: 180px;
}

.playerName {
  position: absolute;
  text-align: center;
  top: 82%;
  left: 50%;
  white-space: nowrap;
  transform: translate(-50%, -50%);
  font-size: 1.3vw;
}

.name {
  position: absolute;
  top: 0%;
  width: 100%;
  height: 20%;
}

.enName {
  position: absolute;
  top: 25%;
  width: 100%;
  height: 20%;
}

.headerText {
  position: absolute;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>