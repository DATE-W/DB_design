<template>
  <div>
    <!-- 顶部导航栏 -->
    <my-nav></my-nav>
    <div>
      <el-row>
        <!-- 新闻（位于左侧） -->
        <el-col :span="18">
          <p class="titleLeagueLeft">足坛八卦</p>
          <el-icon class="Gossipicon">
            <Promotion />
          </el-icon>
          <div class="GossipButton">
            <el-row class="mb-4">
              <el-button plain @click="selectGossip('中超')">中超</el-button>
              <el-button type="primary" plain>英超</el-button>
              <el-button type="success" plain>西甲</el-button>
              <el-button type="info" plain>意甲</el-button>
              <el-button type="warning" plain>德甲</el-button>
              <el-button type="danger" plain>法甲</el-button>
            </el-row>
          </div>
          <div class="line" style="width: 40vw;height: 0.2px;top:-9vh;left:8%;"></div>
          <div v-for="item in GossipNews" :key="item.id" class="itemSearch">
            <div class="imgWrapper">
              <img :src="item.image" alt="Image" class="imgSearch">
            </div>
            <div class="TextWrapper">
              <div class="titleSearch">{{ item.title }}</div>
              <div class="descriptionSearch">{{ item.description }}</div>
            </div>
          </div>
          <div class="NoMore">No More ......</div>
        </el-col>
        <!-- 视频（位于右侧） -->
        <el-col :span="6">
          <p class="titleRight" style="font-size: 25px;">相关视频</p>
          <el-icon class="iconSearch">
            <VideoPlay />
          </el-icon>
          <div class="line" style="width: 22vw;height: 0.2px;top:-3vh;left:-6%;"></div>
          <div class="imgListSearch">
            <div v-for="(item, index) in GossipNewsVideo" :key="index" class="imgItemSearch">
              <el-row :gutter="20">
                <el-col :span="11">
                  <img :src="item.image" alt="Image" class="imgVideoSearch">
                </el-col>
                <el-col :span="8">
                  <div class="descriptionVideoSearch">{{ item.title }}</div>
                </el-col>
              </el-row>
            </div>
          </div>
          <div class="NoMore" style="top:8vh">No More ......</div>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
import MyNav from './nav.vue';
import axios from 'axios';

import carousel1 from '../assets/img/carousel1.png';
import carousel2 from '../assets/img/carousel2.png';
import carousel3 from '../assets/img/carousel3.png';
import carousel4 from '../assets/img/carousel4.png';

export default {
  components: {
    'my-nav': MyNav
  },
  data() {
    return {
      GossipNews: [
        {
          image: carousel1,
          title: '新闻1的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻2的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻3的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel4,
          title: '新闻4的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
      ],
      GossipNewsVideo: [
        {
          image: carousel1,
          title: '新闻1的标题 1111sffsfsfsffsfasfasf',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻2的标题 1111dgdgsgsgsg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻3的标题 1111bbdsgsgsgsgsg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel1,
          title: '新闻4的标题 1111sgszgsgszg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻5的标题 1111sgzsgg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻6的标题 1111dxhdfhdfhdhdhd',
          link: 'https://element-plus.org/'
        },
      ],
    }
  },

  created() {

  },

  methods: {
    //从后端接口获取新闻数据
    async getGossipNews(newsQuantity, tag1, tag2, dataItems) {
      try {
        const requestData = {
          num: newsQuantity,
          matchTag: String(tag1),
          propertyTag: String(tag2),
        };

        const response = await axios.post('/api/Video/GetVideoRandomly', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        console.log(response.data.value);

        // 将数组存贮于传入的数组名中
        dataItems.splice(0, dataItems.length, ...response.data.value);
        // console.log(dataItems);
        // console.log(this.items);
      } catch (error) {
        console.error(error);
      }
    },

    //从后端接口获取视频数据
    async getGossipVideo(newsQuantity, tag1, tag2, dataItems) {
      try {
        const requestData = {
          num: newsQuantity,
          matchTag: String(tag1),
          propertyTag: String(tag2),
        };

        const response = await axios.post('/api/Video/GetVideoRandomly', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        console.log(response.data.value);

        // 将数组存贮于传入的数组名中
        dataItems.splice(0, dataItems.length, ...response.data.value);
        // console.log(dataItems);
        // console.log(this.items);
      } catch (error) {
        console.error(error);
      }
    },

    //按照selectTag来筛选数据
    selectGossip(selectTag) {
      ;
    }
  }
}

</script>

<style>
.itemSearch {
  display: flex;
  /* align-items: center; */
  margin-bottom: 10px;
  position: relative;
  left: 8%;
  top: -13vh;
  margin-top: 5%;
}

.imgWrapper {
  margin-right: 10px;
  width: 60%;
}

.TextWrapper {
  display: flex;
  flex-direction: column;
}

/* 图片样式 */
.imgSearch {
  top: 0vh;
  left: 0%;
  width: 120%;
  height: 120%;
  position: relative;
}

/* 标题样式 */
.titleSearch {
  font-weight: bold;
  width: 50%;
  /* 设置容器的宽度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  white-space: nowrap;
  /* white-space: nowrap;  不换行 */
  margin-top: 5px;
  position: relative;
  left: 8%;
}

/* 描述样式 */
.descriptionSearch {
  width: 60%;
  /* 设置容器的宽度 */
  max-height: 120px;
  /* 设置最大高度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  margin-top: 5px;
  position: relative;
  left: 8%;
}

.iconSearch {
  font-size: 30px;
  position: relative;
  top: -4vh;
  left: 75%;
}

.imgVideoSearch {
  top: 0vh;
  left: -2%;
  width: 90%;
  height: 90%;
  position: relative;
}

.descriptionVideoSearch {
  flex-shrink: 0;
  color: #000;
  font-family: Mogra;
  font-size: 18px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  left: -20%;
  top: 0vh;
  width: 180%;
  /* 设置容器的宽度 */
  max-height: 80%;
  /* 设置最大高度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
}

.NoMore {
  color: #000;
  text-align: center;
  font-family: STSong;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  top: 5%
}

.imgListSearch {
  display: flex;
  flex-direction: column;
  position: relative;
  top: -3vh;
  left: -2vh;
}

.imgItemSearch {
  display: flex;
  align-items: flex-start;
  margin-bottom: 10px;
  position: relative;
  top: 3vh;
}

.titleLeagueLeft {
  width: 364px;
  height: 76px;
  flex-shrink: 0;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 36px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  left: 6vw;
  top: -3vh;
}

.line {
  background: #000000;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25) inset;
  position: relative;
}

.Gossipicon {
  position: relative;
  left: 6vw;
  top: -8vh;
  font-size: 50px;
  color: rgb(134, 71, 228);
}

.GossipButton {
  position: relative;
  left: 10vw;
  top: -12vh;
}

/* 右侧八卦标题 */
.titleRight {
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  top: 2vh;
  left: -6%;
}
</style>