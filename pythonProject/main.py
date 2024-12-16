from fastapi import FastAPI, UploadFile, File, HTTPException
from fastapi.responses import JSONResponse
from PIL import Image
import numpy as np
import tensorflow as tf
import base64
import logging
import io

app = FastAPI()

# Загрузка модели
model_path = 'my_mnist_model.keras'
model = tf.keras.models.load_model(model_path)


def preprocess_image(image_bytes, save_path):
    try:
        img = Image.open(io.BytesIO(image_bytes)).convert("L")

        img_array = np.array(img)

        rows = np.any(img_array == 255, axis=1)
        cols = np.any(img_array == 255, axis=0)
        rmin, rmax = np.where(rows)[0][[0, -1]]
        cmin, cmax = np.where(cols)[0][[0, -1]]

        cropped_img = img.crop((cmin, rmin, cmax, rmax))

        resized_img = cropped_img.resize((28, 28))

        img_array = np.array(resized_img) / 255.0
        img_array = img_array.reshape(1, 28, 28, 1)

        resized_img.save(save_path)
        return img_array

    except Exception as e:
        print(f"Ошибка в preprocess_image: {e}")
        return None


def detect_number(image_bytes):
    try:
        img_array = preprocess_image(image_bytes, "preprocessed_image.png")
        prediction = model.predict(img_array)
        predicted_digit = np.argmax(prediction)
        return predicted_digit
    except Exception as e:
        return None


# @app.post("/detect")
# async def detect(file: UploadFile = File(...)):
#     try:
#         contents = await file.read()
#         predicted_digit = detect_number(contents)
#         if predicted_digit is not None:
#             return {"digit": int(predicted_digit)}
#         else:
#             return JSONResponse(status_code=400, content={"error": "Ошибка обработки изображения"})
#     except Exception as e:
#         return JSONResponse(status_code=500, content={"error": str(e)})

@app.post("/detect")
async def detect(file: UploadFile = File(...)):
    try:
        contents = await file.read()
        logging.info(f"Received file: {len(contents)} bytes")
        try:
            image = np.frombuffer(contents, dtype=np.uint8)
            logging.info(f"Image shape: {image.shape}")
            predicted_digit = detect_number(image)
        except Exception as img_err:
            logging.error(f"Image processing error: {img_err}")
            raise HTTPException(status_code=400, detail="Ошибка обработки изображения")  # Более точный код ошибки

        if predicted_digit is not None:
            return {"digit": int(predicted_digit)}
        else:
            return JSONResponse(status_code=400, content={"error": "Ошибка обработки изображения"})
    except Exception as e:
        logging.exception(f"Internal server error: {e}")  # Логируем исключения с трассировкой
        raise HTTPException(status_code=500, detail=str(e))  # Более точный код ошибки
