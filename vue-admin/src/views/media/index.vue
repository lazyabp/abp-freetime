<template>
  <div class="app-container">
    <div class="filter-container">
      <el-form :inline="true" :model="dataFilter">
        <el-input
          v-model="dataFilter.minSize"
          :placeholder="$t('global.minSize')"
          style="width: 180px;"
        />
        <el-input
          v-model="dataFilter.maxSize"
          :placeholder="$t('global.maxSize')"
          style="width: 180px; margin-left: 10px;"
        />
        <el-date-picker 
          type="daterange" 
          format="yyyy-MM-dd"
          value-format="yyyy-MM-dd"
          :style="{width: '400px', 'margin-left': '10px'}" 
          :start-placeholder="$t('global.starDate')" 
          :end-placeholder="$t('global.endDate')" 
          range-separator="-"
          v-model="dateRange"
        >
        </el-date-picker>
        <el-input
          v-model="dataFilter.filter"
          :placeholder="$t('global.filterString')"
          style="width: 250px; margin-left: 10px;"
        />
        <el-button
          style="margin-left: 10px; text-alignt"
          type="primary"
          @click="refreshPagedData"
        >
          {{ $t('global.search') }}
        </el-button>
        <el-upload 
         :action="uploadAction"
         :on-success="handleUploadSuccess"
         :show-file-list="false"
         :headers="headers"
         :disabled="!checkPermission(['LazyAbp.Media.Create'])"
         :style="{display: 'inline-block', 'margin-left': '10px'}"
        >
          <el-button type="success" icon="el-icon-upload">{{ $t('fileSystem.upload') }}</el-button>
        </el-upload>
      </el-form>
    </div>

    <el-table
      v-loading="dataLoading"
      row-key="id"
      :data="dataList"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      size="small"
    >
      <el-table-column
        label="Md5"
        prop="md5"
        width="260px"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.md5 }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('fileSystem.thumbnail')"
        prop="thumbnail"
        width="80px"
        align="center"
      >
        <template slot-scope="{row}">
          <div v-if="isImage(row)">
            <el-popover trigger="hover" placement="right">
              <img width="400" :src="row.url" alt="">
              <div slot="reference" class="name-wrapper">
                <img width="50" :src="row.url" alt="">
              </div>
            </el-popover>
          </div>
        </template>
      </el-table-column>
      <el-table-column
        label="Url"
        prop="url"
        align="left"
      >
        <template slot-scope="{row}">
          <div>{{ row.url }}</div>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('fileSystem.type')"
        prop="mimeType"
        width="150px"
        align="left"
      >
        <template slot-scope="{row}">
          <span>{{ row.mimeType }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('fileSystem.scene')"
        prop="scene"
        width="100px"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.scene }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('fileSystem.size')"
        prop="size"
        width="150px"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.size }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('LazyAbp.CreationTime')"
        prop="creationTime"
        sortable
        width="150px"
      >
        <template slot-scope="{row}">
          <span>{{ row.creationTime | dateTimeFilter }}</span>
        </template>
      </el-table-column>
      <el-table-column
        :label="$t('LazyAbp.Actions')"
        align="center"
        width="100px"
        min-width="150px"
        fixed="right"
      >
        <template slot-scope="{row}">
          <el-button
            :disabled="!checkPermission(['LazyAbp.Media.Delete'])"
            size="mini"
            type="danger"
            @click="handleDelete(row)"
          >
            {{ $t('LazyAbp.Delete') }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="dataTotal>0"
      :total="dataTotal"
      :page.sync="currentPage"
      :limit.sync="pageSize"
      @pagination="refreshPagedData"
    />
  </div>
</template>

<script lang="ts">
import { abpPagerFormat, dateFormat } from '@/utils'
import DataListMiXin from '@/mixins/DataListMiXin'
import AuthHeaderMiXin from '@/mixins/AuthHeaderMiXin'
import Component, { mixins } from 'vue-class-component'
import MediaApiService, { StreamUploadUrl, MediaDto, MediaListRequestDto } from '@/api/media-api'
import { checkPermission } from '@/utils/permission'
import Pagination from '@/components/Pagination/index.vue'

@Component({
  name: 'Media',
  components: {
    Pagination
  },
  filters: {
    dateTimeFilter(datetime: string) {
      const date = new Date(datetime)
      return dateFormat(date, 'YYYY-mm-dd HH:MM')
    }
  },
  methods: {
    checkPermission
  }
})
export default class extends mixins(DataListMiXin, AuthHeaderMiXin) {

  public dataFilter = new MediaListRequestDto()
  private uploadAction = StreamUploadUrl
  private dateRange = []
  private headers = {}

  mounted() {
    this.refreshPagedData()
    this.headers = this.getAuthHeaders()
  }

  protected processDataFilter() {
    this.dataFilter.skipCount = abpPagerFormat(this.currentPage, this.pageSize)

    if (this.dateRange) {
      this.dataFilter.creationStart = this.dateRange[0]
      this.dataFilter.creationEnd = this.dateRange[1]
    } else {
      this.dataFilter.creationStart = this.dataFilter.creationEnd = undefined
    }
  }

  /** 列表 */
  protected getPagedList(filter: any) {
    return MediaApiService.getMedias(filter)
  }

  private handleUploadSuccess(res: MediaDto, file: any) {
    this.refreshPagedData()
  }

  private isImage(item: MediaDto) {
    if (item.mimeType && item.mimeType.indexOf('image/') !== -1) {
      return true
    }

    if (item.url.lastIndexOf('.') !== -1) {
      let extName = item.url.substring(item.url.lastIndexOf('.')).toLowerCase()

      return ['.jpg', '.jpeg', '.png', '.wbmp', '.gif'].indexOf(extName) !== -1
    }

    return false
  }

  /** 删除 */
  private handleDelete(item: MediaDto) {
    this.$confirm(this.l('LazyAbp.ItemWillBeDeletedMessage'),
      this.l('LazyAbp.ItemWillBeDeletedMessageWithFormat', [ item.md5 ]), {
        callback: (action) => {
          if (action === 'confirm') {
            MediaApiService.deleteMedia(item.id).then(() => {
              this.$message.success(this.l('LazyAbp.Successfully'))
              this.refreshPagedData()
            })
          }
        }
      })
  }
}
</script>
