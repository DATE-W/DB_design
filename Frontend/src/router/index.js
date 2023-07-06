import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../components/main.vue')
    },
    {
      path: '/signin',
      name: 'about',
      component: () => import('../components/signin.vue')
    },
    {
      path: '/signup',
      component: () => import('../components/signup.vue')
    },
    {
      path: '/recover',
      component: () => import('../components/recover.vue')
    },
    {
      path: '/forum',
      component: () => import('../components/forum.vue')
    }
  ]
})

export default router
