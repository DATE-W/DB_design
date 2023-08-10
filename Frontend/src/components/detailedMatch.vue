<!-- 2154314_郑楷_赛事详情页 2023.08.10 16:00 v1.0.0 
v1.0.0 添加页面，改路由，完成赛事列表向该页的UID传值 -->

<template>
<my-nav></my-nav>
<el-button size="large" type="info" icon="back" @click="toGames()">Back</el-button>
<p>{{ gameUid }}</p>
</template>

<script>
import MyNav from './nav.vue';

export default {
  components: {
    'my-nav': MyNav
  },

  created() {
    this.gameUid = this.$route.query.gameUid;
  },

  methods: {
    toGames()
    {
        this.$router.push(
        {
          path:`/Games`,
          query:{           
          }
        }
      );
    },
  },

  data() {
    return {
      gameUid: '',
    }
    
  }
}
</script>