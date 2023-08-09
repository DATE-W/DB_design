<template>
  <my-nav></my-nav>
  <div class="top">
    <img src="../assets/img/pmlogo.png" class="teamIcon" alt="This is team icon">
    <div class="teamInfo">
      <div style="font-size: 2vw;margin-left: 30%;">不要挂ysj挑战</div>

      <el-row :gutter="20">
        <el-col>
          <div>成立: 2023</div>
        </el-col><br><br>

        <el-col>
          <div>国家: 袁国</div>
        </el-col><br><br>

        <el-col>
          <div>地址: 19-301</div>
        </el-col>
      </el-row>

    </div>
  </div>

  <div class="playerInfo">
    <img src="../assets/img/otto.png" class="playerPic" @click="direct2detailedPlayerMsg">
    <p style="margin-left: 4%;">otto</p>
    <p>{{ this.teamName }}</p>
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

  methods: {
    direct2detailedPlayerMsg() {
      this.$router.push('/detailedPlayerMsg');
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

  //    this.temps = [];
//   this.temps = response.data;

    },


  },

  data() {
    return {
      teamName: '',
      address:'',
      country:'',
      city: '',
      homeCourt: '',
      containerNum: 0,
      establishmentTime:'',
      tel:'',
      email:'',
      teamMember: [
        {"playerName":"wjl","playerPhoto":"url"}
      ]


    }
    
  }
}
</script>

<style scoped>
/* 
      成立: 2023 主场: 301 电话: +861145141919810 地址: 19-301
      <p class="middle">国家: 袁国<br><br>容纳: 10人<br><br>邮箱: wyh@qq.com</p>
      <p class="right"></p>
 */

/* 顶部容器 */
.top {
  position: absolute;
  left: 20%;
  width: 60vw;
  height: 25vw;
  flex-shrink: 0;
  background: rgb(230, 230, 230);
}

/* 队标 */
.teamIcon {
  position: absolute;
  left: 8%;
  top: 25%;
  width: 15%;
  height: 40%;
  flex-shrink: 0;
}

/* 队伍信息 */
.teamInfo {
  position: absolute;
  left: 30%;
  top: 10%;
  width: 60%;
  height: 70%;
  flex-shrink: 0;
  background: rgb(230, 230, 230);

}

/* 左侧字 */
.left {
  font-size: 1vw;
}

/* 中侧字 */
.middle {
  font-size: 1vw;
  margin-left: 40%;
}

/* 右侧字 */
.right {
  font-size: 1vw;
  margin-left: 50%;

}

/* 球员信息 */
.playerInfo {
  position: absolute;
  left: 20%;
  top: 60%;
  width: 60%;
  height: 30%;
  flex-shrink: 0;
  background: #ffffff;

}

.playerPic {
  width: 100px;
  height: 150px;
}
</style>