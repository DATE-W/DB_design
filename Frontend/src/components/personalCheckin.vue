<!-- 签到 v1.0 -->
<template>
    <!-- 添加外层的容器，并使用Flexbox布局 -->
    <div class="checkin-container">
      <el-config-provider :locale="zhCn">
        <el-calendar ref="calendar">
          <template #date-cell="{ data }">
            <p>
              {{ formatDate(data.day) }}
              {{ isSelectedDate(data.day) ? '✔️' : '' }}
            </p>
          </template>
        </el-calendar>
      </el-config-provider>
      <el-button class="checkin-button" type="primary">点此签到</el-button>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue'
  import type { CalendarDateType, CalendarInstance } from 'element-plus'
  import zhCn from 'element-plus/lib/locale/lang/zh-cn'
  
  const calendar = ref<CalendarInstance>()
  const selectedDates = ['2023-07-10', '2023-08-01', '2023-08-03'] // 统一使用 "YYYY-MM-DD" 格式
  
  const isSelectedDate = (date: string) => {
    return selectedDates.includes(date)
  }
  
  const formatDate = (date: string) => {
    // 将日期格式化为 "M-D" 的格式
    const [year, month, day] = date.split('-')
    return `${parseInt(month)}-${parseInt(day)}`
  }
  
  const selectDate = (val: CalendarDateType) => {
    if (!calendar.value) return
    calendar.value.selectDate(val)
  }
  </script>
  
  <style scoped>
  .checkin-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100%;
  }
  
  .checkin-button {
    font-size: large;
    width: 200px;
    height: 60px;
  }
  </style>
  