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
              v-model="auditLog.applicationName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.TenantId')">
            <el-input
              v-model="auditLog.tenantId"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.TenantName')">
            <el-input
              v-model="auditLog.tenantName"
              readonly
            />
          </el-form-item>
          <el-form-item
            v-if="auditLog.impersonatorTenantId !== null"
            :label="$t('LazyAbp.ImpersonatorTenantId')"
          >
            <el-input
              v-model="auditLog.impersonatorTenantId"
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
              v-model="auditLog.userId"
              readonly
            />
          </el-form-item>
          <el-form-item
            v-if="auditLog.impersonatorUserId !== null"
            :label="$t('LazyAbp.ImpersonatorUserId')"
          >
            <el-input
              v-model="auditLog.impersonatorUserId"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.UserName')">
            <el-input
              v-model="auditLog.userName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientId')">
            <el-input
              v-model="auditLog.clientId"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientName')">
            <el-input
              v-model="auditLog.clientName"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ClientIpAddress')">
            <el-input
              v-model="auditLog.clientIpAddress"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.BrowserInfo')">
            <el-input
              v-model="auditLog.browserInfo"
              type="textarea"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('LazyAbp.Operation')"
          name="operation"
        >
          <el-form-item :label="$t('LazyAbp.RequestUrl')">
            <el-input
              v-model="auditLog.url"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.HttpMethod')">
            <el-input
              v-model="auditLog.httpMethod"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ExecutionTime')">
            <el-input
              :value="getFormatDateTime(auditLog.executionTime)"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.ExecutionDuration')">
            <el-input
              v-model="auditLog.executionDuration"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.HttpStatusCode')">
            <el-input
              v-model="auditLog.httpStatusCode"
              readonly
            />
          </el-form-item>
          <el-form-item :label="$t('LazyAbp.CorrelationId')">
            <el-input
              v-model="auditLog.correlationId"
              readonly
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane
          :label="$t('LazyAbp.InvokeMethod')"
          name="methodInvoke"
          style="height:300px;overflow-y:auto;overflow-x:hidden;"
        >
          <el-timeline class="timeline-card">
            <el-timeline-item
              v-for="(action, index) in getActions"
              :key="index"
              :timestamp="getFormatDateTime(action.executionTime)"
              :type="auditLog.httpStatusCode | httpStatusCodeTimelineFilter"
              placement="top"
            >
              <el-card>
                <el-form-item :label="$t('LazyAbp.ServiceName')">
                  <el-input
                    v-model="action.serviceName"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.MethodName')">
                  <el-input
                    v-model="action.methodName"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.ExecutionDuration')">
                  <el-input
                    v-model="action.executionDuration"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.Parameters')">
                  <json-editor
                    :value="getFormatJsonValue(action.parameters)"
                  />
                </el-form-item>
              </el-card>
            </el-timeline-item>
          </el-timeline>
        </el-tab-pane>
        <el-tab-pane
          v-if="getEntitiesChanges.length > 0"
          :label="$t('LazyAbp.EntitiesChanged')"
          name="entitiesChanged"
          style="height:300px;overflow-y:auto;overflow-x:hidden;"
        >
          <el-timeline class="timeline-card">
            <el-timeline-item
              v-for="(entity, index) in getEntitiesChanges"
              :key="index"
              :timestamp="getFormatDateTime(entity.changeTime)"
              :type="entity.changeType | entityChangeTypeTimelineFilter"
              placement="top"
            >
              <el-card>
                <el-form-item :label="$t('LazyAbp.ChangeType')">
                  <el-input
                    :value="getEntityChangeTypeName(entity.changeType)"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.EntityTypeFullName')">
                  <el-input
                    v-model="entity.entityTypeFullName"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.EntityId')">
                  <el-input
                    v-model="entity.entityId"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.TenantId')">
                  <el-input
                    v-model="entity.entityTenantId"
                    readonly
                  />
                </el-form-item>
                <el-form-item :label="$t('LazyAbp.PropertyChanges')">
                  <el-table
                    row-key="id"
                    :data="entity.propertyChanges"
                    border
                    fit
                    highlight-current-row
                    style="width: 100%;"
                  >
                    <el-table-column
                      :label="$t('LazyAbp.PropertyName')"
                      prop="propertyName"
                      sortable
                      width="200px"
                    >
                      <template slot-scope="{row}">
                        <span>{{ row.propertyName }}</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      :label="$t('LazyAbp.NewValue')"
                      prop="newValue"
                      sortable
                      width="320px"
                    >
                      <template slot-scope="{row}">
                        <span>{{ row.newValue }}</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      :label="$t('LazyAbp.OriginalValue')"
                      prop="originalValue"
                      sortable
                      width="320px"
                    >
                      <template slot-scope="{row}">
                        <span>{{ row.originalValue }}</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      :label="$t('LazyAbp.PropertyTypeFullName')"
                      prop="propertyTypeFullName"
                      sortable
                      width="500px"
                    >
                      <template slot-scope="{row}">
                        <span>{{ row.propertyTypeFullName }}</span>
                      </template>
                    </el-table-column>
                  </el-table>
                </el-form-item>
              </el-card>
            </el-timeline-item>
          </el-timeline>
        </el-tab-pane>
        <el-tab-pane
          v-if="hasException"
          :label="$t('LazyAbp.Exception')"
          name="exception"
        >
          <el-form-item :label="$t('LazyAbp.StackTrack')">
            <el-input
              v-model="auditLog.exceptions"
              type="textarea"
              :rows="15"
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
            v-for="(key, index) in Object.keys(auditLog.extraProperties)"
            :key="index"
            :label="key"
          >
            <el-input
              v-model="auditLog.extraProperties[key]"
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
import AuditingService, { Action, AuditLog, EntityChange, ChangeType } from '@/api/auditing'
import { dateFormat } from '@/utils'
import JsonEditor from '@/components/JsonEditor/index.vue'

const entityChangeTypeNameMap: { [key: number]: string } = {
  [ChangeType.Created]: 'AbpAuditLogging.Created',
  [ChangeType.Updated]: 'AbpAuditLogging.Updated',
  [ChangeType.Deleted]: 'AbpAuditLogging.Deleted'
}
const entityChangeTypeTimelineMap: { [key: number]: string } = {
  [ChangeType.Created]: 'success',
  [ChangeType.Updated]: 'warning',
  [ChangeType.Deleted]: 'danger'
}

@Component({
  name: 'AuditLogProfile',
  components: {
    JsonEditor
  },
  filters: {
    httpStatusCodeTimelineFilter(httpStatusCode: number) {
      if (httpStatusCode >= 200 && httpStatusCode < 300) {
        return 'success'
      }
      if (httpStatusCode >= 300 && httpStatusCode < 500) {
        return 'warning'
      }
      if (httpStatusCode >= 500) {
        return 'danger'
      }
      return 'primary'
    },
    entityChangeTypeTimelineFilter(type: ChangeType) {
      return entityChangeTypeTimelineMap[type]
    }
  },
  computed: {
    getFormatDateTime() {
      return (dateTime: any) => {
        return dateFormat(new Date(dateTime), 'YYYY-mm-dd HH:MM:SS:NS')
      }
    },
    getFormatJsonValue() {
      return (jsonString: string) => {
        if (jsonString !== '') {
          return JSON.parse(jsonString)
        }
        return null
      }
    },
    getEntityChangeTypeName() {
      return (type: ChangeType) => {
        return this.$t(entityChangeTypeNameMap[type])
      }
    }
  }
})
export default class extends Vue {
  @Prop({ default: '' })
  private auditLogId!: string

  private auditLog = new AuditLog()
  private activedTabItem = 'application'

  @Watch('auditLogId', { immediate: true })
  private onAuditLogIdChanged() {
    if (this.auditLogId) {
      AuditingService.getAuditLogById(this.auditLogId).then(res => {
        this.auditLog = res
        this.activedTabItem = 'application'
      })
    }
  }

  get hasExtraProperties() {
    if (this.auditLog.extraProperties) {
      return Object.keys(this.auditLog.extraProperties).length > 0
    }
    return false
  }

  get hasException() {
    if (this.auditLog.exceptions) {
      return true
    }
    return false
  }

  get getActions() {
    if (this.auditLog.actions && this.auditLog.actions.length > 0) {
      return this.sortByDateTime(this.auditLog.actions, 'executionTime')
    }
    return new Array<Action>()
  }

  get getEntitiesChanges() {
    if (this.auditLog.entityChanges && this.auditLog.entityChanges.length > 0) {
      return this.sortByDateTime(this.auditLog.entityChanges, 'changeTime')
    }
    return new Array<EntityChange>()
  }

  private sortByDateTime(array: Array<any>, sortField: string) {
    return array.sort((obj1, obj2) => {
      if (obj1[sortField] && obj2[sortField]) {
        const time1 = new Date(obj1[sortField])
        const time2 = new Date(obj2[sortField])
        return time1.getTime() - time2.getTime()
      }
      return 0
    })
  }
}
</script>

<style lang="scss" scoped>
.timeline-card {
  margin: 5px;
}
</style>
