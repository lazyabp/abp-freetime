import axios from 'axios'
import i18n from '@/lang/index'
import { MessageBox, Notification } from 'element-ui'
import { UserModule } from '@/store/modules/user'
import { AbpModule } from '@/store/modules/abp'
import { getLanguage, getXsrfToken } from '@/utils/cookies'

const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  timeout: 30000
  // withCredentials: true // send cookies when cross-domain requests
})

// Request interceptors
service.interceptors.request.use(
  (config) => {
    if (AbpModule.configuration) {
      if (AbpModule.configuration.currentTenant.isAvailable) {
        config.headers['__tenant'] = AbpModule.configuration.currentTenant.id
      }
      config.headers['Accept-Language'] = AbpModule.configuration.localization?.currentCulture?.cultureName
    } else {
      // abp官方类库用的 zh-Hans 的简体中文包 这里直接粗暴一点
      // 顺序调整到token之前
      const language = getLanguage()
      if (language?.indexOf('zh') !== -1) {
        config.headers['Accept-Language'] = 'zh-Hans'
      } else {
        config.headers['Accept-Language'] = language
      }
    }
    if (config.url === '/connect/token') {
      return config
    }
    // Add X-Access-Token header to every request, you can add other custom headers here
    if (UserModule.token) {
      config.headers.Authorization = UserModule.token
    }
    
    const xsrfToken = getXsrfToken()
    if (xsrfToken) {
      config.headers['RequestVerificationToken'] = xsrfToken
    }

    return config
  },
  (error) => {
    Promise.reject(error)
  }
)

function l(name: string) {
  return i18n.tc(name)
}

function showError(error: any) {
  let message = ''
  let title = ''
  let response = error.response
  console.log(response)
  // 特别处理返回数据为blob类型时的错误信息
  if (error.config.responseType && error.config.responseType === 'blob') {
    const fileReader = new FileReader()
    fileReader.onload = (e) => {
      if (e.target?.result) {
        const jsonError = JSON.parse(e.target.result.toString())
        Notification({
          title: l('AbpUi.DefaultErrorMessage'),
          message: jsonError.error.message,
          type: 'error',
          duration: 5 * 1000
        })
      }
    }
    fileReader.readAsText(error.response.data, 'utf-8')
    return
  }
  if (response.data && response.data.error) {
    if (response.data.error_description) {
      title = response.data.error
      message = response.data.error_description
    } else if (response.data.error.details) {
      message = response.data.error.details
      title = response.data.error.message
    } else if (response.data.error.message) {
      message = response.data.error.message
    }
  } else {
    switch (response.status) {
      case 401:
        title = l('AbpUi.DefaultErrorMessage401')
        message = l('AbpUi.DefaultErrorMessage401Detail')
        break
      case 403:
        title = l('AbpUi.DefaultErrorMessage403')
        message = l('AbpUi.DefaultErrorMessage403Detail')
        break
      case 404:
        title = l('AbpUi.DefaultErrorMessage404')
        message = l('AbpUi.DefaultErrorMessage404Detail')
        break
      case 429:
        message = l('global.operatingFast')
        break
      case 500:
        title = l('AbpUi.500Message')
        message = l('AbpUi.InternalServerErrorMessage')
        break
      default:
        // 从响应中返回原始状态说明
        title = response.statusText
        message = response.status + ' ' + response.statusText
        break
    }
  }
  Notification({
    title: title,
    message: message,
    type: 'error',
    duration: 5 * 1000
  })
}

// Response interceptors
service.interceptors.response.use(
  (response) => {
    return response
  },
  (error) => {
    if (error.response.status === 401) {
      UserModule.RefreshSession().then(token => {
        const config = error.response.config
        config.headers.Authorization = token
        return service(config)
      }).catch(() => {
        MessageBox.confirm(
          l('login.tokenExprition'),
          l('login.confirmLogout'),
          {
            confirmButtonText: l('login.relogin'),
            cancelButtonText: l('global.cancel'),
            type: 'error'
          }).then(() => {
          UserModule.ResetToken()
          location.reload() // To prevent bugs from vue-router
        })
      })
    } else {
      showError(error)
    }
    return Promise.reject(error)
  }
)

export default service
