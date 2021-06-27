<template>
  <el-dialog
    :title="$t('fileSystem.upload')"
    :visible="showDialog"
    :close-on-click-modal="false"
    width="640px"
    custom-class="modal-form"
    @close="onFormClosed"
  >
    <el-upload
      class="upload-demo"
      ref="upload"
      drag
      :action="fileUploadUrl"
      :on-success="handleUploadSuccess"
      :headers="headers"
      :accept="accept"
      :limit="limit"
      :file-list="fileList"
      :auto-upload="false"
      :on-exceed="onMaxExceed"
      multiple>
      <i class="el-icon-upload"></i>
      <div class="el-upload__text">{{ $t('fileSystem.uploadTips') }}</div>
      <div class="el-upload__tip" slot="tip">{{ $t('fileSystem.maxfilesLimit', { max: limit }) }}</div>
    </el-upload>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onFormClosed">{{ $t('global.close') }}</el-button>
      <el-button type="warning" @click="onStartUpload">{{ $t('fileSystem.upload') }}</el-button>
      <el-button type="primary" @click="onConfirm">{{ $t('global.confirm') }}</el-button>
    </span>
  </el-dialog>
</template>

<script lang="ts">
import { Upload } from 'element-ui'
import { Component, Mixins, Prop, Watch } from 'vue-property-decorator'
import { StreamUploadUrl, MediaDto } from '@/api/media-api'
import AuthHeaderMiXin from '@/mixins/AuthHeaderMiXin'
import LocalizationMiXin from '@/mixins/LocalizationMiXin'

@Component({
  name: 'MediaUploadDialog'
})
export default class extends Mixins(LocalizationMiXin, AuthHeaderMiXin) {
  @Prop({ default: 9 })
  private limit!: number

  @Prop({ default: 'image/*' })
  private accept!: string

  @Prop({ default: false })
  private showDialog!: boolean

  private fileUploadUrl = StreamUploadUrl
  private files: MediaDto[] = []
  private fileList: any = []
  private headers: any = {}

  @Watch('showDialog', { immediate: true })
  private onShowDialogChanged() {
    this.files = []
    this.fileList = []
    this.headers = this.getAuthHeaders()
  }

  public close() {
    this.files = []
    this.fileList = []
  }

  private handleUploadSuccess(res: MediaDto, file: any) {
    this.files.push(res)
    // this.$emit('onFileUploaded', res)
  }

  private onMaxExceed() {
    this.$message.error(this.l('maxfilesLimit', { max: this.limit }));
  }

  private onStartUpload() {
    const uploader = this.$refs.upload as Upload
    uploader.submit()
  }

  private onConfirm() {
    if (this.limit === 1) {
      this.$emit('onFileUploaded', this.files.pop())
    } else {
      this.$emit('onFileUploaded', this.files)
    }
  }

  private onFormClosed() {
    this.$emit('closed', [])
  }
}
</script>

<style lang="scss">
.el-upload--text .el-upload-dragger {
  width: 600px;
}
</style>
