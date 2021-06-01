<template>
  <div class="app-container">
    <h4>{{ $t('AbpAccount.ChangePassword') }}</h4>
    <el-divider />
    <el-form>
      <el-form-item :label="$t('AbpAccount.DisplayName:CurrentPassword')" prop="currentPassword">
        <el-input v-model.trim="updatePassword.currentPassword" type="password" />
      </el-form-item>
      <el-form-item :label="$t('AbpAccount.DisplayName:NewPassword')" prop="newPassword">
        <el-input v-model.trim="updatePassword.newPassword" type="password" />
      </el-form-item>
      <el-form-item :label="$t('AbpAccount.DisplayName:NewPasswordConfirm')" prop="confirmPassword">
        <el-input v-model.trim="confirmPassword" type="password" />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          @click="submit"
        >
          修改密码
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import MyProfileService, { ChangePassword } from '@/api/profile'

@Component({
  name: 'SetPassword'
})
export default class extends Vue {
  private updatePassword = new ChangePassword()

  private confirmPassword: string = ''

  private submit() {
    if (!this.updatePassword.currentPassword) {
      this.$message.error(this.$t('AbpAccount.CurrentPasswordIsEmpty').toString())
      return
    }

    if (!this.updatePassword.newPassword) {
      this.$message.error(this.$t('AbpAccount.NewPasswordIsEmpty').toString())
      return
    }

    if (this.updatePassword.newPassword !== this.confirmPassword) {
      this.$message.error(this.$t('AbpAccount.NewPasswordConfirmFailed').toString())
      return
    }

    MyProfileService.changePassword(this.updatePassword).then(() => {
      this.$message.success(this.$t('AbpAccount.ChangePasswordSucceed').toString())
    });
  }
}
</script>
