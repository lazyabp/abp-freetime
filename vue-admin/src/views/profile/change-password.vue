<template>
  <div class="app-container">
    <set-password />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import SetPassword from './components/SetPassword.vue'

@Component({
  name: 'ChangePassword',
  components: {
    SetPassword
  }
})
export default class extends Vue {

}
</script>
