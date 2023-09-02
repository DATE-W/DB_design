<template>
  <my-nav></my-nav>
  <el-card class="teamNameAndIcon">
    <img class="teamIconContainer" alt="This is team icon" :src="this.logo">
    <div class="teamNameContainer">
      {{ this.teamName }}
    </div>
    <div class="teamEnnameContainer">
      {{ this.enName }}
    </div>
  </el-card>

  <el-card class="teamInfo">
    <div class="firstBar">
      <div class="firstBlock">
        成立：{{ this.foundYear }}
      </div>
      <div class="secondBlock">
        国家：{{ this.country }}
      </div>
      <div class="thirdBlock">
        联赛类别：{{ this.leagueType }}
      </div>
      <div class="fourthBlock">
        主教练：{{ this.coach }}
      </div>
    </div>

    <div class="secondBar">
      <div class="firstBlock">
        城市：{{ this.city }}
      </div>
      <div class="secondBlock">
        地址：{{ this.address }}
      </div>

      <div class="thirdBlock">
        主场：{{ this.venueName }}
      </div>
      <div class="fourthBlock">
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

    <div class="briefIntroductionContainer">
      球队简介：{{ this.teamName }}足球俱乐部（英文名：{{ this.enName }}），是{{ this.leagueType }}的一支足球俱乐部。
      {{ this.teamName }}成立于{{ this.foundYear }}年，坐落于美丽的{{ this.city }}。{{ this.teamName }}的主场球场为
      {{ this.venueName }}，能够容纳{{ this.venueCapacity }}人同时观赛。
    </div>
  </el-card>

  <div class="titleContainer">
    <div class="titleBar"></div>
    <div class="title">
      <p style="transform: translateY(-30%); ">球员信息</p>
    </div>
  </div>

  <el-table :data="teamMember" style="width: 70vw; left: 16vw; top: 70vh;">
    <el-table-column align="center" prop="playerNumber" label="号码" width="120" />
    <el-table-column align="center" prop="playerPosition" label="位置" width="120" />


    <el-table-column>
      <template #default="scope">
        <div>
          <img :src="scope.row.playerPhoto" alt="Player Photo" class="player-photo">
        </div>
      </template>
    </el-table-column>

    <el-table-column align="left" prop="playerName" label="姓名" width="230" />




    <el-table-column align="center" prop="playerAppearance" label="出场" width="120" />
    <el-table-column align="center" prop="playerShoot" label="射门" width="120" />
    <el-table-column align="center" prop="playerGoal" label="进球" width="120" />
    <el-table-column align="center" prop="playerNationality" label="国籍" width="120" />

  </el-table>

  <!--


  
  <div class="eventInfo">
    <div style="font-size: 2vw;margin-left: 42%;">
      球员列表
    </div>

    <div class="positionInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        位置
      </p>
    </div>

    <div class="numberInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        号码
      </p>
    </div>

    <div class="photoInfo">
    </div>

    <div class="nameInfo">
      <p style="position: absolute;left: 10%;  transform: translate(-50%, -50%);">
        姓名
      </p>
    </div>

    <div class="appearanceInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        出场
      </p>
    </div>

    <div class="shootInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        射门
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        进球
      </p>
    </div>

    <div class="nationalityInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        国籍
      </p>
    </div>

    
  </div>

  <div class="detailedEventData" v-for="(singleTeamMember, index) in teamMember" :key="index"
    :style="{ top: `${index * 2.5 + 31.5}rem` }" @click="direct2detailedPlayerMsg(singleTeamMember.playerName)">
    <div class="positionInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerPosition }}
      </p>
    </div>

    <div class="numberInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerNumber }}
      </p>
    </div>

    <div class="photoInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerPhoto }}
      </p>
    </div>

    <div class="nameInfo">
      <p style="position: absolute;left: 10%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerName }}
      </p>
    </div>


    <div class="appearanceInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerAppearance }}
      </p>
    </div>

    <div class="shootInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerShoot }}
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ singleTeamMember.playerGoal }}
      </p>
    </div>

    
  </div>--> <!-- -->
  <!--
  <el-card class="playerInfo" v-for="(singleteamMember, index) in teamMember" :key="index"
    :style="{ top: `${Math.floor(index / 4) * 19 + 26}rem`, left: `${index % 4 * 16 + 16}rem` }"
    @click="direct2detailedPlayerMsg(singleteamMember.playerName)">
    <img class="playerPic" :src="singleteamMember.playerPhoto">
    <p class="playerName" style="left: 50%; transform: translate(-50%, -50%); font-size: 1.2vw;">
      {{ singleteamMember.playerName }}
    </p>
  </el-card>-->
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
      this.teamName = teamName;
      try {
        response = await axios.post('/api/updateTeam/getTeamInfoByName', {
          teamName: teamName,
        });

        //        console.log(response);
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
      leagueType: '紫砂',

      teamMember: [
        { playerName: "wjl", playerPhoto: "/src/assets/img/otto.png", "playerPosition": "前锋", "playerNumber": "12", "playerAppearance": "3", "playerShoot": "12", "playerGoal": "2", "playerNationality": "US", },
        { playerName: "wyh", playerPhoto: "/src/assets/img/otto.png", "playerPosition": "前锋", "playerNumber": "12", "playerAppearance": "3", "playerShoot": "12", "playerGoal": "2", "playerNationality": "US", },
        { playerName: "wrb", playerPhoto: "/src/assets/img/otto.png", "playerPosition": "前锋", "playerNumber": "12", "playerAppearance": "3", "playerShoot": "12", "playerGoal": "2", "playerNationality": "US", },
        { playerName: "wh", playerPhoto: "/src/assets/img/otto.png", "playerPosition": "前锋", "playerNumber": "12", "playerAppearance": "3", "playerShoot": "12", "playerGoal": "2", "playerNationality": "US", },
        { playerName: "111", playerPhoto: "/src/assets/img/otto.png", "playerPosition": "前锋", "playerNumber": "12", "playerAppearance": "3", "playerShoot": "12", "playerGoal": "2", "playerNationality": "US", },

      ],

      players: [
        { "name": '球员1', "photoUrl": 'https://chunithm.sega.jp/$site/images/pc/prev.png.webp' },
        { "name": '球员2', "photoUrl": 'https://chunithm.sega.jp/$site/images/pc/prev.png.webp' },
        // 在这里继续添加更多球员的信息
      ],
    }

  }
}
</script>

<style scoped>
/* 顶部容器 */
.teamNameAndIcon {
  position: absolute;
  left: 15vw;
  width: 70vw;
  height: 20vh;
  flex-shrink: 0;
}

.teamIconContainer {
  position: absolute;
  left: 4vw;
  top: 4vh;
  width: 8vw;
  height: 12vh;
  flex-shrink: 0;
}

.teamNameContainer {
  position: absolute;
  left: 15vw;
  top: 4vh;
  font-size: 2.5vw;
}

.teamEnnameContainer {
  position: absolute;
  left: 15vw;
  top: 11vh;
  font-size: 1.5vw;
}

.teamInfo {
  position: absolute;
  left: 15vw;
  top: 32vh;
  width: 70vw;
  height: 35vh;
  flex-shrink: 0;
}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 4vw;
  top: 5vh;
  height: 4vh;
  width: 60vw;
}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 4vw;
  top: 11vh;
  height: 4vh;
  width: 60vw;
}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 4vw;
  top: 17vh;
  height: 4vh;
  width: 60vw;
}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 16vw;
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 30vw;
}

.fourthBlock {
  position: absolute;
  left: 42vw;
}

.briefIntroductionContainer {
  position: absolute;
  left: 3vw;
  top: 23vh;
  height: 13vh;
  width: 62vw;
}


.titleContainer {
  position: absolute;
  border: none;
  top: 70vh;
  left: 15vw;
  width: 7vw;
  height: 6vh;
  background: rgb(240, 240, 240);
}

.titleBar {
  position: absolute;
  top: 1.5vh;
  width: 0.5vw;
  height: 3vh;
  background: aqua;
}

.title {
  position: absolute;
  left: 1.5vw;
  width: 10vw;
  height: 6vh;
}

.cell-content {
  display: flex;
  align-items: center;
}

.image {
  width: 40px;
  height: 40px;
  margin-right: 10px;
}




.nationalityInfo {
  position: absolute;
  left: 53vw;
  width: 7vw;
  height: 4vh;
}

.detailedEventData {
  position: absolute;
  left: 10vw;
  background: wheat;
}

.player-photo {
  width: 50px;
  height: 50px;
  object-fit: cover;
  border-radius: 20%;
}
</style>