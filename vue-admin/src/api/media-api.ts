import { urlStringify } from '@/utils/index'
import ApiService from './serviceBase'
import { PagedAndSortedResultRequestDto, PagedResultDto } from './types'

const serviceUrl = process.env.VUE_APP_BASE_API

export const StreamUploadUrl = serviceUrl + '/api/media/upload/stream'

export default class MediaApiService {
  public static getMedias(payload: MediaListRequestDto) {
    let _url = '/api/media?'
    _url += urlStringify(payload)

    return ApiService.Get<PagedResultDto<MediaDto>>(_url, serviceUrl)
  }

  public static getMediaById(id: string) {
    let _url = '/api/media/' + id

    return ApiService.Get<MediaDto>(_url, serviceUrl)
  }

  public static getMediaByMd5(md5: string) {
    let _url = '/api/media/by-md5' + md5

    return ApiService.Get<MediaDto>(_url, serviceUrl)
  }

  public static createMedia(body: MediaCreateDto) {
    let _url = '/api/media'

    return ApiService.Post<MediaDto>(_url, body, serviceUrl)
  }

  public static updateMedia(id: string, body: MediaCreateDto) {
    let _url = '/api/media/' + id

    return ApiService.Put<MediaDto>(_url, body, serviceUrl)
  }

  public static deleteMedia(id: string) {
    let _url = '/api/media/' + id

    return ApiService.Delete(_url, serviceUrl)
  } 

  public static uploadByUrl(body: FastDFSFileUrlRequestDto) {
    let _url = '/api/media/upload/url'

    return ApiService.Post<MediaDto>(_url, body, serviceUrl)
  }

  public static uploadByUrlsBulk(body: FastDFSFileUrlsRequestDto) {
    let _url = '/api/media/upload/bulk-urls'

    return ApiService.Post<MediaDto>(_url, body, serviceUrl)
  }

  public static uploadByBase64(body: FastDFSFileBase64RequestDto) {
    let _url = '/api/media/upload/base64'

    return ApiService.Post<MediaDto>(_url, body, serviceUrl)
  }

}

export class FastDFSFileBase64RequestDto {
  scene?: string | undefined
  path?: string | undefined
  name?: string | undefined
  mediaType?: string | undefined
  fileName?: string | undefined
  data?: string | undefined
}

export class FastDFSFileUrlRequestDto {
  scene?: string | undefined
  path?: string | undefined
  url?: string | undefined
}

export class FastDFSFileUrlsRequestDto {
  scene?: string | undefined
  path?: string | undefined
  urls?: string[] | undefined
}

export class MediaCreateDto {
  url?: string | undefined
  md5?: string | undefined
  mimeType?: string | undefined
  path?: string | undefined
  domain?: string | undefined
  scene?: string | undefined
  size?: number
  mtime?: number
  scenes?: string | undefined
  src?: string | undefined
}

export class MediaDto {
  id!: string
  creationTime?: Date
  creatorId?: string | undefined
  url!: string
  md5?: string | undefined
  mimeType?: string | undefined
  path?: string | undefined
  domain?: string | undefined
  scene?: string | undefined
  size?: number
  mtime?: number
  scenes?: string | undefined
  src?: string | undefined
  readonly extraProperties?: { [key: string]: any } | undefined
}

export class MediaListRequestDto extends PagedAndSortedResultRequestDto {
  creationStart?: Date | undefined
  creationEnd?: Date | undefined
  minSize?: number | undefined
  maxSize?: number | undefined
  filter?: string | undefined
}
