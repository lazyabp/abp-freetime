<template>
  <div>
    <el-form label-width="100px">
      <el-tabs v-model="activedTabItem">
        <el-tab-pane
          :label="$t('LazyAbp.Application')"
          name="application"
        >
          <el-form-item :label="$t('LazyAbp.ApplicationName')">
            <el-input
              v-model="securityLog.applicationName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.TenantName')">
            <el-input
              v-model="securityLog.tenantName"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('LazyAbp.UserInfo')"
          name="userInfo"
        >
          <el-form-item :label="$t('LazyAbp.UserId')">
            <el-input
              v-model="securityLog.userId"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.UserName')">
            <el-input
              v-model="securityLog.userName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientId')">
            <el-input
              v-model="securityLog.clientId"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientName')">
            <el-input
              v-model="securityLog.clientName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientIpAddress')">
            <el-input
              v-model="securityLog.clientIpAddress"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.BrowserInfo')">
            <el-input
              v-model="securityLog.browserInfo"
              type="textarea"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('LazyAbp.Operation')"
          name="operation"
        >
          <el-form-item :label="$t('LazyAbp.Identity')">
            <el-input
              v-model="securityLog.identity"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ActionName')">
            <el-input
              v-model="securityLog.action"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.CreationTime')">
            <el-input
              :value="getFormatDateTime(securityLog.creationTime)"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane
          v-if="hasExtraProperties"
          :label="$t('LazyAbp.Additional')"
          name="extraProperties"
        >
          <el-form-item
            v-for="(key, index) in Object.keys(securityLog.extraProperties)"
            :key="index"
            :label="key"
          >
            <el-input
              v-model="securityLog.extraProperties[key]"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
    </el-form>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from 'vue-property-decorator'
import AuditingService, { SecurityLog } from '@/api/auditing'
import { dateFormat } from '@/utils'

@Component({
  name: 'SecurityLogProfile',
  computed: {
    getFormatDateTime() {
      return (dateTime: any) => {
        return dateFormat(new Date(dateTime), 'YYYY-mm-dd HH:MM:SS:NS')
      }
    }
  }
})
export default class extends Vue {
  @Prop({ default: '' })
  private securityLogId!: string

  private securityLog = new SecurityLog()
  private activedTabItem = 'application'

  get hasExtraProperties() {
    if (this.securityLog.extraProperties) {
      return Object.keys(this.securityLog.extraProperties).length > 0
    }
    return false
  }

  @Watch('securityLogId', { immediate: true })
  private onSecurityLogChanged() {
    if (this.securityLogId) {
      AuditingService.getSecurityLogById(this.securityLogId).then(res => {
        this.securityLog = res
        this.activedTabItem = 'application'
      })
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
