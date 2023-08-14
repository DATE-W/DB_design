<template>
  <my-nav></my-nav>
  <div class="topContainer">
    <img class="teamIcon" alt="This is team icon" :src="this.teamLogo">
    <div class="teamInfo">
      <p class="header">
        {{ this.teamName }}
      </p>

      <div class="firstBar">
        <div class="firstBlock">
          成立：{{ this.establishmentTime }}
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
          主场：{{ this.homeCourt }}
        </div>
        <div class="secondBlock">
          容纳：{{ this.containerNum }}人
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

    </div>
  </div>

  <div class="playerInfo" v-for="(teamMember, index) in teamMembers" :key="index"
    :style="{ top: `${Math.floor(index / 4) * 18 + 26}rem`, left: `${index % 4 * 16 + 16}rem` }"
    @click="direct2detailedPlayerMsg(teamMember.playerName)">
    <img class="playerPic" :src="teamMember.playerPhoto">
    <p class="playerName">
      {{ teamMember.playerName }}
    </p>
  </div>
</template>



<script>
import MyNav from './nav.vue';

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
        response = await axios.post('api', {
          teamName: teamName,
        });

      } catch (err) {
        ElMessage({
          message: '获取球队信息失败',
          type: 'error',
        });
      }
      this.teamMembers = [];
      this.address = response.data.address;
      this.country = response.data.country;
      this.city = response.data.city;
      this.homeCourt = response.data.homeCourt;
      this.containerNum = response.data.containerNum;
      this.establishmentTime = response.data.establishmentTime;
      this.tel = response.data.tel;
      this.email = response.data.email;
      this.teamMembers = response.data.teamMember;

    },


  },

  data() {
    return {
      teamName: '不要挂ysj挑战',
      teamLogo: '/src/assets/img/pmlogo.png',
      address: '须弥城114弄514号',
      country: '璃月',
      city: '龙门',
      homeCourt: '301',
      containerNum: 114514,
      establishmentTime: '2023',
      tel: '+86 12345678901',
      email: 'wjl@qq.com',
      teamMembers: [
        { "playerName": "wjl", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyhwyhwyh", "playerPhoto": "/src/assets/img/otto.png" },
        { "playerName": "wyh", "playerPhoto": "/src/assets/img/otto.png" },

      ]


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
  flex-shrink: 0;  /*

  background: rgb(194, 249, 249); */

}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 3vw;
  top: 8vh;
  height: 4vh;
  width: 36vw;  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 3vw;
  top: 14vh;
  height: 4vh;
  width: 36vw;  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 3vw;
  top: 21vh;
  height: 4vh;
  width: 36vw;  /* 

  background: rgb(0, 240, 249);*/

}

/* 信息第四行 */
.fourthBar {
  position: absolute;
  left: 3vw;
  top: 28vh;
  height: 4vh;
  width: 36vw;  /*

  background: rgb(0, 240, 249); */

}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;  /*

  background: rgb(255, 255, 255); */
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 16vw;  /*

  background: rgb(255, 255, 255); */
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 26vw;  /*

  background: rgb(255, 255, 255); */
}

/* 球员信息 */
.playerInfo {
  position: absolute;
  left: 20%;
  top: 60%;
  width: 200px;
  height: 240px;
  flex-shrink: 0;  /*

  background: #0fffff; */
}

.playerPic {
  position: absolute;
  left: 20%;
  width: 120px;
  height: 180px;
}

.playerName {
  position: absolute;
  text-align: center;
  top: 75%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 1.3vw;
}

.header {
  position: absolute;
  font-size: 2vw;
  left: 50%; 
  transform: translate(-50%, -50%);
}
</style>