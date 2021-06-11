import { Component, Vue } from 'vue-property-decorator'
import { UserModule } from '@/store/modules/user'
import { AbpModule } from '@/store/modules/abp'
import { getXsrfToken } from '@/utils/cookies'

@Component
export default class AuthHeaderMiXin extends Vue {
  /**
   * 获取登录用户的http请求头信息
   */
  public getAuthHeaders() {
    let headers: { [key: string]: any } = {}

    if (AbpModule.configuration) {
      if (AbpModule.configuration.currentTenant.isAvailable) {
        headers['__tenant'] = AbpModule.configuration.currentTenant.id
      }

      if (UserModule.token) {
        headers['Authorization'] = UserModule.token
      }
      
      const xsrfToken = getXsrfToken()
      if (xsrfToken) {
        headers['RequestVerificationToken'] = xsrfToken
      }
    }

    return headers;
  }
}
