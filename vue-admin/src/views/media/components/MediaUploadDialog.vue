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
      :file-list="files"
      :auto-upload="false"
      :on-exceed="onMaxExceed"
      multiple>
      <i class="el-icon-upload"></i>
      <div class="el-upload__text">{{ $t('fileSystem.uploadTips') }}</div>
      <div class="el-upload__tip" slot="tip">{{ $t('fileSystem.maxfilesLimit', { max: limit }) }}</div>
    </el-upload>
    <span slot="footer" class="dialog-footer">
      <el-button @click="onFormClosed">{{ $t('global.close') }}</el-button>
      <el-button type="primary" @click="onSubmit">{{ $t('fileSystem.upload') }}</el-button>
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
  private files: any = {}
  private headers: any = {}

  @Watch('showDialog', { immediate: true })
  private onShowDialogChanged() {
    this.files = []
    this.headers = this.getAuthHeaders()
  }

  public close() {
    this.files = []
  }

  private handleUploadSuccess(res: MediaDto, file: any) {
    this.files.push({
      name: res.src,
      url: res.url
    })

    this.$emit('onFileUploaded', res)
  }

  private onMaxExceed() {
    this.$message.error(this.l('maxfilesLimit', { max: this.limit }));
  }

  private onSubmit() {
    const uploader = this.$refs.upload as Upload
    uploader.submit()
  }

  private onFormClosed() {
    this.$emit('closed', this.files)
  }
}
</script>

<style lang="scss">
.el-upload--text .el-upload-dragger {
  width: 600px;
}
</style>
