import { RouteConfig } from 'vue-router'
import Layout from '@/layout/index.vue'

const commonRouter: RouteConfig = {
  path: '/common',
  component: Layout,
  meta: {
    title: 'othersManagement',
    icon: 'box',
    roles: ['LazyAbp.Media'], // you can set roles in root nav
    alwaysShow: true // will always show the root menu
  },
  children: [
    {
      path: 'media',
      component: () => import(/* webpackChunkName: "common" */ '@/views/media/index.vue'),
      name: 'media',
      meta: {
        title: 'filemanagement',
        icon: 'icon',
        roles: ['LazyAbp.Media']
      }
    }
  ]
}

export default commonRouter
