import { ListResultDto, PagedResultDto, PagedAndSortedResultRequestDto, FullAuditedEntityDto } from './types'
import ApiService from './serviceBase'
import { urlStringify } from '@/utils'

const serviceUrl = process.env.VUE_APP_BASE_API

export default class TenantService {
  public static findTenantByName(name: string) {
    let _url = '/api/abp/multi-tenancy/tenants/by-name/'
    _url += name
    return ApiService.Get<FindTenantResult>(_url, serviceUrl)
  }

  public static findTenantById(id: string) {
    let _url = '/api/abp/multi-tenancy/tenants/by-id/'
    _url += id
    return ApiService.Get<FindTenantResult>(_url, serviceUrl)
  }

  public static getTenantById(id: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id
    return ApiService.Get<TenantDto>(_url, serviceUrl)
  }

  public static getTenants(payload: TenantGetByPaged) {
    let _url = '/api/tenant-management/tenants?'
    _url += urlStringify(payload)
    return ApiService.Get<PagedResultDto<TenantDto>>(_url, serviceUrl)
  }

  public static createTenant(payload: TenantCreateOrEdit) {
    const _url = '/api/tenant-management/tenants'
    return ApiService.Post<TenantDto>(_url, payload, serviceUrl)
  }

  public static changeTenantName(id: string, name: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id
    return ApiService.Put<TenantDto>(_url, { name: name }, serviceUrl)
  }

  public static deleteTenant(id: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id
    return ApiService.Delete(_url, serviceUrl)
  }

  public static getTenantConnections(id: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id + '/connection-string'
    return ApiService.Get<ListResultDto<TenantConnectionString>>(_url, serviceUrl)
  }

  public static getTenantConnectionByName(id: string, name: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id + '/connection-string/'
    _url += name
    return ApiService.Get<TenantConnectionString>(_url, serviceUrl)
  }

  public static setTenantConnection(id: string, payload: TenantConnectionString) {
    let _url = '/api/tenant-management/tenants/'
    _url += id + '/connection-string'
    return ApiService.Put<TenantConnectionString>(_url, payload, serviceUrl)
  }

  public static deleteTenantConnectionByName(id: string, name: string) {
    let _url = '/api/tenant-management/tenants/'
    _url += id + '/connection-string/'
    _url += name
    return ApiService.Delete(_url, serviceUrl)
  }
}

/** ???????????????????????? */
export class TenantGetByPaged extends PagedAndSortedResultRequestDto {
  /** ?????????????????? */
  filter: string | undefined

  constructor() {
    super()
    this.filter = ''
    this.sorting = ''
    this.skipCount = 1
  }
}

/** ?????????????????? */
export class TenantCreateOrEdit {
  /** ????????????????????? */
  adminEmailAddress = ''
  /** ??????????????? */
  adminPassword = ''
  /** ???????????? */
  name = ''

  public static empty() {
    return new TenantCreateOrEdit()
  }
}

/** ???????????? */
export class TenantDto extends FullAuditedEntityDto {
  /** ???????????? */
  id!: string
  /** ???????????? */
  name!: string
}

/** ????????????????????? */
export class TenantConnectionString {
  /** ?????? */
  name!: string
  /** ??? */
  value!: string

  public static empty() {
    const tenantConnection = new TenantConnectionString()
    tenantConnection.name = ''
    tenantConnection.value = ''
    return tenantConnection
  }
}

export class FindTenantResult {
  name!: string
  tenantId!: string
  success!: boolean
}
