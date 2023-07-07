<!-- 论坛-上传帖子-v1.0 -->
<template>
    <div class="common-layout">
      <my-nav></my-nav>
      <!-- 导航栏 -->
      <el-container>
        <el-aside width="200px"> </el-aside>
        <!-- 空出左侧，使更美观 -->
        <el-main>
          <div class="icon-text-container">
            <el-icon class="edit-icon">
              <Edit />
            </el-icon>
            <span class="text-content">发布新帖</span>
          </div>
          <!-- 此div是发布新贴的图标 -->
          <div class="title-tag-container">
            <el-input v-model="postTitle" placeholder="请输入帖子标题" size="large" class="edit-title" clearable>
            </el-input>
            <el-dropdown @command="handleCommand">
              <span class="el-dropdown-link">
                选择标签<el-icon class="el-icon--right"><arrow-down /></el-icon>
              </span>
              <template #dropdown>
                <el-dropdown-menu>
                  <el-dropdown-item command="diy">自定义标签</el-dropdown-item>
                  <el-dropdown-item command="西甲">西甲</el-dropdown-item>
                  <el-dropdown-item command="德甲">德甲</el-dropdown-item>
                  <el-dropdown-item command="意甲">意甲</el-dropdown-item>
                  <el-dropdown-item command="法甲">法甲</el-dropdown-item>
                  <el-dropdown-item command="中超">中超</el-dropdown-item>
                </el-dropdown-menu>
              </template>
            </el-dropdown>
          </div>
          <!-- 输入标题和标签 -->
          <div class="text-container">
            <el-input v-model="postText" :rows="10" type="textarea" placeholder="请填写内容" />
          </div>
          <!-- 输入帖子内容 -->
          <div class="pic-container">
            <el-button type="primary" @click="openFilePicker">上传图片</el-button>
            <div class="selected-pics-container">
              <div v-for="pic in selectedPics" :key="pic.name">
                <img :src="pic" alt="Selected Pic" width="100" height="100">
              </div>
            </div>
          </div>
          <!-- 上传图片 -->
        </el-main>
        <el-aside width="200px"> </el-aside>
        <!-- 空出右侧 -->
      </el-container>
    </div>
  </template>
  
  <script>
  import { defineComponent } from 'vue';
  import MyNav from './nav.vue';
  
  export default defineComponent({
    components: {
      'my-nav': MyNav
    },
    data() {
      return {
        postTitle: '',
        postText: '',
        selectedTags: [], // 存储已选择的标签
        selectedPics: []  // 存储已选择的图片
      };
    },
    methods: {
      openFilePicker() {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.multiple = true;
        input.addEventListener('change', this.handleFileSelect);
        input.click();
      },
      handleFileSelect(event) {
        const files = event.target.files;
        for (let i = 0; i < files.length; i++) {
          const file = files[i];
          const reader = new FileReader();
          reader.onload = (e) => {
            this.selectedPics.push(e.target.result);
          };
          reader.readAsDataURL(file);
        }
        console.log(this.selectedPics); // 调试输出选择的图片
      },
      handleCommand(command) {
        if (command === 'diy') {
          this.postTitle += '##';
        } else {
          if (!this.selectedTags.includes(command)) {
            this.selectedTags.push(command);
            this.postTitle += `#${command}#`;
          }
        }
      }
    }
  });
  </script>
  
  <style scoped>
  .common-layout {
    display: flex;
    flex-direction: column;
    height: 100vh;
  }
  
  /* 主体部分 */
  
  .el-container {
    flex: 1;
    display: flex;
  }
  
  .el-main {
    flex: 1;
    background-color: #F5F7FA;
    padding-top: 20px;
  }
  
  /* 编写帖子内容部分 */
  
  .icon-text-container {
    display: flex;
    align-items: center;
    font-weight: bold;
  }
  /* 图标行 */
  
  .title-tag-container {
    display: flex;
    align-items: center;
    margin-bottom: 30px;
  }
  /* 标题标签行 */
  
  .edit-icon {
    font-size: 24px;
  }
  /* 图标 */
  
  .text-content {
    margin-left: 8px;
  }
  /* 使“发布帖子”和icon有一定距离 */
  
  .edit-title {
    margin-top: 20px;
    width: 70%;
    margin-right: 20px;
  }
  /* 标题的输入框 */
  
  .text-container{
    width: 80%;
  }
  /* 内容的输入框 */
  
  .selected-pics-container {
    display: flex;
    flex-wrap: wrap;
  }
  
  .selected-pics-container > div {
    margin-right: 10px;
    margin-bottom: 10px;
  }
</style>
  