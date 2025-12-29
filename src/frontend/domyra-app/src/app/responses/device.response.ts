import { Device } from "../models/device.model";
import { BaseResponse } from "./base.response";

export interface DeviceResponse extends BaseResponse {
    device?: Device,
}