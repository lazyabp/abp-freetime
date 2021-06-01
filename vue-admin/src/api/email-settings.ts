import { urlStringify } from '@/utils'

import ApiService from './serviceBase'
import { PagedAndSortedResultRequestDto, PagedResultDto } from './types'

const serviceUrl = process.env.VUE_APP_BASE_API

export default class EmailSettingsApiService {
  public static getEmailSettings(payload: EmailSettingsGetByPaged) {
    let _url = '/api/setting-management/emailing?'
    _url += urlStringify(payload)
    return ApiService.Get<PagedResultDto<EmailSetting>>(_url, serviceUrl)
  }

  public static setEmailSettings(payload: EmailSettingsUpdate) {
    const _url = '/api/setting-management/emailing'
    return ApiService.Put<void>(_url, payload, serviceUrl)
  }
}

export class EmailSetting {
  smtpHost!: string
  smtpPort!: number
  smtpUserName!: string
  smtpPassword!: string
  smtpDomain!: string
  smtpEnableSsl!: boolean
  smtpUseDefaultCredentials!: boolean
  defaultFromAddress?: string
  defaultFromDisplayName?: string
}

export class EmailSettingsUpdate extends EmailSetting {

}

export class EmailSettingsGetByPaged extends PagedAndSortedResultRequestDto {

}

