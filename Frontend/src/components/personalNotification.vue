<!-- 消息通知 v1.0 -->
<template>
    <div class="notice-container">
      <div class="title-container">
        <el-icon class="message-icon">
          <Message />
        </el-icon>
        <span class="notice-title">通知中心</span>
        <el-button class="clear-button" type="text" @click="clearConfirm">清空通知</el-button>
      </div>
      <hr class="separator">
      <div class="message-container">
        <div>
          <span class="message-title">最近收到的通知</span>
          <el-dropdown class="message-menu">
            <span class="el-dropdown-link">
              {{ selectedCategory }}
              <el-icon class="el-icon--right">
                <arrow-down />
              </el-icon>
            </span>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item @click="selectCategory('全部类别')">全部类别</el-dropdown-item>
                <el-dropdown-item @click="selectCategory('赞同&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;')">赞同</el-dropdown-item>
                <el-dropdown-item @click="selectCategory('评论&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;')">评论</el-dropdown-item>
                <el-dropdown-item @click="selectCategory('站务通知')">站务通知</el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </div>
        <hr class="separator">
        <!-- 以上为通知中心标题及下拉菜单 -->
        <div v-for="(date, index) in uniqueDates" :key="index">
          <div class="notice-date">
            <span>{{ date }}</span>
          </div>
          <div v-for="(notificationIndex, innerIndex) in getNotificationsByDate(date)" :key="innerIndex">
            <div class="notice-item">
              <el-icon v-if="isLikeContent(notice_contents[notificationIndex])" class="icon-likes">
                <Mouse />
              </el-icon>
              <el-icon v-else-if="isCommentContent(notice_contents[notificationIndex])" class="icon-comment">
                <ChatLineSquare />
              </el-icon>
              <el-icon v-else-if="isOfficialContent(notice_people[notificationIndex])" class="icon-official">
                <ChromeFilled />
              </el-icon>
              <span class="notice-people">{{ notice_people[notificationIndex] }}</span>
              <span>&nbsp;&nbsp;</span>
              <span class="notice-content">{{ notice_contents[notificationIndex] }}</span>
            </div>
          </div>
          <hr class="separator" v-if="index !== uniqueDates.length - 1">
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { ElMessage, ElMessageBox } from 'element-plus';
  export default {
    data() {
      return {
        notice_dates: ['2023-08-04', '2023-08-03', '2023-08-03', '2023-08-02', '2023-08-01', '2023-08-01', '2023-08-01', '2023-07-21'],
        notice_people: ['神里凌华', '钟离', '站务派蒙', '宵宫', '门酱', '荒泷一斗', '甘雨', '界孙权'],
        notice_contents: [
          '赞同了您的帖子',
          '发表了一条评论',
          '论坛将于明天进行维护',
          '赞同了您的帖子',
          '发表了一条评论',
          '赞同了您的帖子',
          '发表了一条评论',
          '赞同了您的帖子',
        ],
        selectedCategory: '全部类别'
      };
    },
    computed: {
      uniqueDates() {
        return [...new Set(this.notice_dates)];
      },
    },
    methods: {
      getNotificationsByDate(date) {
        return this.notice_dates.reduce((result, notificationDate, index) => {
          if (notificationDate === date) {
            result.push(index);
          }
          return result;
        }, []);
      },//根据日期获取通知的索引数组，将同日期的一起展示
      selectCategory(category) {
        this.selectedCategory = category;
      },//下拉菜单的选择
      isLikeContent(content) {
        return content.includes('赞同');
      },//检查内容是否为“赞同”相关内容
      isCommentContent(content) {
        return content.includes('评论');
      },// 检查内容是否为“评论”相关内容
      isOfficialContent(content) {
        return content.includes('站务');
      },// 检查内容是否为“评论”相关内容
      clearConfirm() {
        ElMessageBox.confirm('点击确认以清空所有通知记录', '提示', {
          confirmButtonText: '确认',
          cancelButtonText: '取消',
          type: 'warning',
        })
          .then(() => {
            // 用户点击确认按钮时执行的操作
            this.clearNotice();
            this.$message({
              type: 'success',
              message: '通知已成功清空',
            });
          })
          .catch(() => {
            // 用户点击取消按钮时执行的操作，这里可以不用做任何事情
          });
      },
      clearNotice() {
        this.notice_dates = [];
        this.notice_people = [];
        this.notice_contents = [];
      },
    }
  };
  </script>
  
  <style scoped>
  .notice-container {
    align-items: center;
    font-weight: bold;
  }
  
  .title-container {
    align-items: center;
    display: flex;
  }
  
  .message-icon {
    font-size: 45px;
  }
  
  .notice-title {
    margin-left: 8px;
    font-size: 23px;
  }
  
  .clear-button {
    margin-left: 950px;
    margin-bottom: -10px;
  }
  
  .message-container {
    margin-top: 20px;
    align-items: center;
    font-weight: bold;
    width: 70%;
    margin: 0 auto;
  }
  
  .message-title {
    font-weight: bold;
    font-size: 18px;
  }
  
  .separator {
    width: 100%;
    border: 0.5px solid #e6e6e6;
    margin: 10px auto;
  }
  
  .message-menu {
    margin-top: 5px;
    margin-left: 580px;
  }
  
  .notice-date {
    color: #cccccc;
    font-size: 14px;
    margin-bottom: 20px;
  }
  
  .notice-item {
    margin-bottom: 10px;
  }
  
  .notice-people {
    font-size: 17px;
    font-weight: bold;
  }
  
  .notice-content {
    font-family: SimHei;
    font-size: 16px;
    font-weight: normal;
  }
  
  .icon-likes {
    font-size: 20px;
    background-color: #ffff80;
    border-radius: 50%;
    padding: 10px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    margin-right: 8px;
  }
  
  .icon-comment {
    font-size: 20px;
    background-color: #b3d9ff;
    border-radius: 50%;
    padding: 10px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    margin-right: 8px;
  }
  
  .icon-official {
    font-size: 20px;
    background-color: #99ffbb;
    border-radius: 50%;
    padding: 10px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    margin-right: 8px;
  }</style>