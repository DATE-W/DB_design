<template>
  <div class="my-posts-container">
      <div class="posts-section">
      <h2>我的帖子</h2>
      <div class="post-list">
        <div v-for="post in posts" :key="post.id" class="post-item" @mouseover="handleMouseOver" @mouseout="handleMouseOut">
          <div class="post-title">{{ post.title }}</div>
          <div class="post-content">{{ post.content }}</div>
        </div>
      </div>
      </div>
      <div class="points-section">
      <div class="points-box">
          <h3 style="color: aliceblue;">我的积分</h3>
          <div class="points">{{ myPoints }}</div>
      </div>
      </div>
  </div>
  </template>

  <script>
  export default {
    data() {
      return {
        posts: [
          { id: 1, title: "第一个帖子", content: "我太爱wh了!!!" },
          { id: 2, title: "第二个帖子", content: "我可太爱wh了!!!" },
          { id: 3, title: "第三个帖子", content: "我可太太爱wh了!!!" },
          { id: 4, title: "第四个帖子", content: "我可太太太爱wh了!!!" },
          { id: 5, title: "第五个帖子", content: "我可太太太太爱wh了!!!" },
          { id: 6, title: "第六个帖子", content: "我可太太太太太爱wh了!!!" },
          { id: 7, title: "第七个帖子", content: "我可太太太太太太爱wh了!!!" },
          { id: 8, title: "第八个帖子", content: "我可太太太太太太太爱wh了!!!" },
          { id: 9, title: "第九个帖子", content: "我可太太太太太太太太爱wh了!!!" },
          { id: 10, title: "第十个帖子", content: "我可太太太太·········太太太爱wh了!!!" },
          // ...其他帖子数据
        ],
        myPoints: 491 // 假设我的积分为491
      };
    },
    methods: {
    handleMouseOver(event) {
      const postItem = event.currentTarget;
      postItem.classList.add("hovered");
    },
    handleMouseOut(event) {
      const postItem = event.currentTarget;
      postItem.classList.remove("hovered");
    },
  }
  };
  </script>
  
<style scoped>
.my-posts-container {
  display: flex;
  width: 100%;
}

.posts-section {
  flex: 7;
  padding: 20px;
}

.post-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
  overflow-y: auto;
  max-height: 500px;
  align-items: center;
}

.post-item {
  width: 80%;
  border: 1px solid #ccc;
  padding: 10px;
  border-radius: 8px;
  transition: transform 0.3s, box-shadow 0.3s, background-color 0.3s;
}

.post-item.hovered {
  background-color: #f9f9f9;
  transform: scale(1.02);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
  
.post-title {
  font-size: 18px;
  font-weight: bold;
}

.post-content {
  margin-top: 5px;
}

.post-item:nth-child(odd) {
      background: linear-gradient(45deg, #B3E0FF, #66CCFF);
    }

    .post-item:nth-child(even) {
      background: linear-gradient(45deg, #66CCFF, #33B5FF);
    }

/* 积分 */

.points-section {
  flex: 3;
  display: flex;
  justify-content: center;
  align-items: center;
  /* background-color: #f0f0f0; */
  height: 200px;
}

.points-box {
  text-align: center;
  padding: 20px;
  width: 150px;
  height: 150px;
  border-radius: 50%;
  background: linear-gradient(45deg, #008cff, #366ff4);
}

.points {
  font-size: 36px;
  font-weight: bold;
  color: white;
  margin-top: 10px;
}
</style>
  