<template>
  <el-dialog
    v-el-draggable-dialog
    width="800px"
    :visible="showDialog"
    :title="$t('LazyAbp.Client:New')"
    custom-class="modal-form"
    :show-close="false"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    @close="onFormClosed(false)"
  >
    <el-form
      ref="formClient"
      label-width="100px"
      :model="client"
    >
      <el-tabs>
        <el-tab-pane :label="$t('LazyAbp.Basics')">
          <el-form-item
            prop="clientId"
            :label="$t('LazyAbp.Client:Id')"
            :rules="{
              required: true,
              message: $t('pleaseInputBy', {key: $t('LazyAbp.Client:Id')}),
              trigger: 'blur'
            }"
          >
            <el-input
              v-model="client.clientId"
              :placeholder="$t('pleaseInputBy', {key: $t('LazyAbp.Client:Id')})"
            />
          </el-form-item>
          <el-form-item
            prop="clientName"
            :label="$t('LazyAbp.Name')"
            :rules="{
              required: true,
              message: $t('pleaseInputBy', {key: $t('LazyAbp.Name')}),
              trigger: 'blur'
            }"
          >
            <el-input
              v-model="client.clientName"
              :placeholder="$t('pleaseInputBy', {key: $t('LazyAbp.Name')})"
            />
          </el-form-item>
          <el-form-item
            prop="description"
            :label="$t('LazyAbp.Description')"
          >
            <el-input
              v-model="client.description"
            />
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane :label="$t('LazyAbp.Client:AllowedGrantTypes')">
          <grant-type-edit-form
            v-model="client.allowedGrantTypes"
          />
        </el-tab-pane>
      </el-tabs>
      <el-form-item>
        <el-button
          class="cancel"
          type="info"
          @click="onFormClosed(false)"
        >
          {{ $t('LazyAbp.Cancel') }}
        </el-button>
        <el-button
          class="confirm"
          type="primary"
          icon="el-icon-check"
          @click="onSave"
        >
          {{ $t('LazyAbp.Save') }}
        </el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import { Component, Mixins, Prop } from 'vue-property-decorator'
import LocalizationMiXin from '@/mixins/LocalizationMiXin'

import { Form } from 'element-ui'
import GrantTypeEditForm from './GrantTypeEditForm.vue'

import ClientApiService, { ClientCreate } from '@/api/clients'

@Component({
  name: 'ClientCreateForm',
  components: {
    GrantTypeEditForm
  }
})
export default class ClientCreateForm extends Mixins(LocalizationMiXin) {
  @Prop({ default: false })
  private showDialog!: boolean

  @Prop({ default: () => { return new Array<string>() } })
  private supportedGrantypes!: string[]

  private client = new ClientCreate()

  private onSave() {
    const clientEditForm = this.$refs.formClient as Form
    clientEditForm.validate(valid => {
      if (valid) {
        ClientApiService
          .create(this.client)
          .then(() => {
            this.$message.success(this.l('global.successful'))
            this.onFormClosed(true)
          })
      }
    })
  }

  private onFormClosed(changed: boolean) {
    const clientEditForm = this.$refs.formClient as Form
    clientEditForm.resetFields()
    this.$emit('closed', changed)
  }
}
</script>

<style lang="scss" scoped>
.full-select {
  width: 100%;
}
.confirm {
  position: absolute;
  right: 10px;
  width:100px;
}
.cancel {
  position: absolute;
  right: 120px;
  width:100px;
}
</style>
