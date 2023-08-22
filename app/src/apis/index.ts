import { appConfigs } from 'common/consts/configs';
import axiosInstance from './https';
import * as nswag from './nswag';

const authClient = new nswag.AuthClient(appConfigs.apiUrl, axiosInstance);
const userClient = new nswag.UserClient(appConfigs.apiUrl, axiosInstance);

export{
  authClient,
  userClient
}
