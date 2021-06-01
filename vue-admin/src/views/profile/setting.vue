<template>
  <my-profile />
</template>

<script lang="ts">
import MyProfile from './components/MyProfile.vue'
//import MyNotifier from './components/MyNotifier.vue'
import { Component, Vue } from 'vue-property-decorator'

@Component({
  name: 'ProfileSettingEditForm',
  components: {
    MyProfile,
    //MyNotifier
  }
})
export default class extends Vue {
}
</script>
